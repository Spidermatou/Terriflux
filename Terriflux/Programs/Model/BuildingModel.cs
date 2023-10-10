using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Terriflux.Programs.Data.Management;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// Represents a building
    /// </summary>
    public partial class BuildingModel : IPlaceable, IVerbosable
    {
        private static readonly Dictionary<InfluenceScale, int> MULTIPLICATION_RATE = new()
        {
            { InfluenceScale.REGIONAL, 1 },
            { InfluenceScale.NATIONAL, 3 },
            { InfluenceScale.WORLDWIDE, 5 },

        }; // the larger the scale, the larger the needs and production will be 

        private readonly List<IBuildingObserver> observers = new();
        private int[] impacts = new int[3];
        private Dictionary<FlowKind, int> needs = new();
        private Dictionary<FlowKind, int> products = new();
        private readonly List<CellModel> parts = new();      // cells wich compose the building
        private InfluenceScale actualInfluenceScale;
        private Direction2D orientation; 
        private string name = "Building";


        // CREATION
        public BuildingModel(
            string name,
            int size,
            Dictionary<FlowKind, int> needs,
            Dictionary<FlowKind, int> products,
            Direction2D direction
            )
        {
            // build attributes
            SetName(name);
            SetInfluence(InfluenceScale.REGIONAL);  // by default
            SetBasisNeeds(needs);
            SetBasisProduction(products);
            SetDirection(direction);

            // build the building with cells
            for (int i = 0; i < size; i++)
            {
                CellModel part = new(name, CellKind.BUILDING);
                this.parts.Add(part);
            }
        }

        public static BuildingModel CreateFromName(string name) 
        {
            StreamReader reader = DataManager.LoadBuildingData();
            name = name.ToLower().Replace(" ", ""); // erase spaces

            // read each line to find the correct building
            string line;
            string[] split;
            string line_name;
            string[] splited_products;
            string[] splited_needs;

            int size;
            Dictionary<FlowKind, int> effective_products = new();
            Dictionary<FlowKind, int> effective_needs = new();
            Direction2D direction;

            while ((line = reader.ReadLine()) != null)
            {
                split = line.Split(";");

                // extract name
                line_name = split[0].Replace(" ", "").ToLower();    
                
                // is it the one we want? 
                if (line_name.Equals(name))     // yes
                {
                    // extract size (nb of parts)
                    size = int.Parse(split[1]);

                    // extract needs
                    splited_needs = split[2].Replace(" ", "").Split(",");
                    for (int i = 0; i < splited_needs.Length - 1; i += 2)
                    {
                        if (!effective_needs.ContainsKey(GlobalTools.TranslateToFlowKind(splited_needs[i])))
                        {
                            effective_needs.Add(GlobalTools.TranslateToFlowKind(splited_needs[i]), int.Parse(splited_needs[i + 1]));
                        }
                    }

                    // extract products
                    splited_products = split[3].Replace(" ", "").Split(",");
                    for (int i = 0; i < splited_products.Length - 1; i++)
                    {
                        if (!effective_products.ContainsKey(GlobalTools.TranslateToFlowKind(splited_products[i])))
                        {
                            effective_products.Add(GlobalTools.TranslateToFlowKind(splited_products[i]), int.Parse(splited_products[i + 1]));
                        }
                    }

                    // extract default orientation
                    string direction_code = split[4].Trim().ToUpper();
                    if (direction_code.Length != 1)
                    {
                        throw new FileLoadException("The \"default direction\" code is not valid " +
                        "within the file. Usage: h/H (for horizontal) or v/V (for vertical)");
                    }
                    else
                    {
                        if (direction_code[0] == 'H')
                        {
                            direction = Direction2D.HORIZONTAL;
                        }
                        else
                        {
                            direction = Direction2D.VERTICAL;
                        }
                    }

                    return new BuildingModel(name, size, effective_needs, effective_products, direction);
                }
                // no? skip to next building
            }

            // correct name never founded
            throw new ArgumentException($"No building with the name '{name}' has been" +
                    "found among the available buildings");
        }

        // Building's PRIMARY INFORMATIONS
        /// <summary>
        /// Modify building's name, and the name of all cells wich compose him.
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            this.name = name;

            // update self composition
            foreach (CellModel part in this.parts)
            {
                part.SetCellName(this.name);
            }

            this.NotifyName();
        }

        public string GetName()
        {
            return this.name;
        }

        public void NotifyName()
        {
            foreach (IBuildingObserver observer in this.observers)
            {
                observer.UpdateName(this.GetName());
            }
        }

        // Building's COMPOSITION
        public int GetPartsNumber()
        {
            return this.parts.Count;
        }
        
        // Building's PRODUCTION
        /// <summary>
        /// Modify building productions, then apply influence scale's modificators
        /// </summary>
        /// <param name="products">Default building productions, at lowest scale of influence</param>
        public void SetBasisProduction(Dictionary<FlowKind, int> products)
        {
            this.products = products;

            // apply influence multiplicators
            foreach (KeyValuePair<FlowKind, int> kvp in this.products)
            {
                this.products[kvp.Key] = kvp.Value * MULTIPLICATION_RATE[this.GetInfluence()]; 
            }

            NotifyProducts();
        }

        public void NotifyProducts()
        {
            foreach (IBuildingObserver observer in observers)
            {
                if (observer is IExtendedBuildingObserver extendedObserver)
                {
                    // clone
                    extendedObserver.UpdateProducts(this.products.ToDictionary(entry => entry.Key,
                                                                      entry => entry.Value));
                }
            }
        }

        public FlowKind[] GetFlowsProduction()
        {
            return products.Keys.ToArray();
        }

        public int GetQuantityProducedOf(FlowKind kind)
        {
            return needs[kind];
        }

        // Building's NEEDS
        /// <summary>
        /// Modify building needs, then apply influence scale's modificators
        /// </summary>
        /// <param name="needs">Default building needs, at lowest scale of influence</param>
        public void SetBasisNeeds(Dictionary<FlowKind, int> needs)
        {
            this.needs = needs;

            // apply influence multiplicators
            foreach (KeyValuePair<FlowKind, int> kvp in this.needs)
            {
                this.needs[kvp.Key] = kvp.Value * MULTIPLICATION_RATE[this.GetInfluence()];
            }

            NotifyNeeds();
        }

        public void NotifyNeeds()
        {
            foreach (IBuildingObserver observer in observers)
            {
                if (observer is IExtendedBuildingObserver extendedObserver)
                {
                    // clone
                    extendedObserver.UpdateNeeds(this.needs.ToDictionary(entry => entry.Key,
                                                                      entry => entry.Value));
                }
            }
        }

        public int GetQuantityNeeded(FlowKind kind)
        {
            return needs[kind];
        }

        public FlowKind[] GetFlowsNeeded()
        {
            return needs.Keys.ToArray();
        }

        // Building's INFLUENCE scale
        public void SetInfluence(InfluenceScale influence)
        {
            actualInfluenceScale = influence;
            NotifyInfluence();
        }

        public void NotifyInfluence()
        {
            foreach (IBuildingObserver observer in observers)
            {
                if (observer is IExtendedBuildingObserver extendedObserver)
                {
                    extendedObserver.UpdateInfluence(this.actualInfluenceScale);
                }
            }
        }

        public InfluenceScale GetInfluence()
        {
            return actualInfluenceScale;
        }

        // Building's IMPACTS
        public void SetImpacts(ICollection<int> impacts)
        {
            if (impacts.Count == 3)
            {
                this.impacts = (int[])impacts;
            }
            else
            {
                if (impacts.Count < 3) throw new ArgumentException("Try to attribute too few impacts ! Required: 3 exactly");
                else throw new ArgumentException("Try to attribute too much impacts ! Required: 3 exactly");
            }
        }
        public int[] GetImpacts()
        {
            return impacts;
        }

        public void NotifyImpacts()
        {
            foreach (IBuildingObserver observer in observers)
            {
                if (observer is IExtendedBuildingObserver extendedObserver)
                {
                    extendedObserver.UpdateImpacts(impacts);
                }
            }
        }

        // PLACEMENT
        public Direction2D GetDirection()
        {
            return this.orientation;
        }

        public void SetDirection(Direction2D direction)
        {
            this.orientation = direction;
            NotifyDirection();
        }

        private void NotifyDirection()
        {
            foreach (IBuildingObserver observer in observers)
            {
                observer.UpdateDirection(this.orientation);
            }
        }

        // ALLS
        public void CallForUpdates()
        {
            // cells infos
            foreach (CellModel part in parts)
            {
                part.NotifyAlls();
            }

            // builds infos
            NotifyName();       // for all
            
            NotifyImpacts();    // for ExtendedObservers
            NotifyInfluence();
            NotifyNeeds();
            NotifyProducts();
        }

        // OBSERVATION
        public void AddObserver(IBuildingObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(IBuildingObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        /// <summary>
        /// Add observers who will look at the cells composing the building
        /// </summary>
        /// <param name="observers"></param>
        public void AddCompositionObserver(ICellObserver[] observers)
        {
            // security
            if (observers.Length != this.GetPartsNumber())
            {
                throw new ArgumentException("Try to assign more or less cells' observers than cells wich compose the building !");
            }

            // update
            List<ICellObserver> observersList = observers.ToList();
            for (int i = 0; i < observersList.Count; i++)
            {
                this.parts[i].AddObserver(observersList[i]);
            }
        }

        /// <summary>
        /// Remove observers who will look at the cells composing the building
        /// </summary>
        /// <param name="observers"></param>
        public void RemoveCompositionObserver(ICellObserver[] observers)
        {
            // security
            if (observers.Length != this.GetPartsNumber())
            {
                throw new ArgumentException("Try to assign more or less cells' observers than cells wich compose the building !");
            }

            // update
            List<ICellObserver> observersList = observers.ToList();
            for (int i = 0; i < observersList.Count; i++)
            {
                this.parts[i].RemoveObserver(observersList[i]);
            }
        }

        public string Verbose()
        {
            StringBuilder sb = new();

            // basis
            sb.Append($"Name: {this.name}\n");
            sb.Append($"Actual orientation: {this.orientation}\n");
            sb.Append($"Nb of observers: {this.observers.Count}\n");
            sb.Append($"Influence: {this.actualInfluenceScale}\n");

            // impacts
            sb.Append($"Impacts:\n");
            sb.Append($"    Sociability{impacts[0]}\n");
            sb.Append($"    Economy{impacts[1]}\n");
            sb.Append($"    Ecology{impacts[2]}\n");

            // parts
            sb.Append($"Composed with {this.GetPartsNumber()}\n");
            for (int i = 0; i< this.GetPartsNumber(); i++)
            {
                sb.Append($"    part-{i} correctly instantiated? {this.parts[i] != null}\n");
            }

            // needs
            sb.Append($"Needs:\n");
            foreach (KeyValuePair<FlowKind, int> kvp in this.needs)
            {
                sb.Append($"    Key={kvp.Key}, Value{kvp.Value}\n");
            }

            // products
            sb.Append($"Products:\n");
            foreach (KeyValuePair<FlowKind, int> kvp in this.products)
            {
                sb.Append($"    Key={kvp.Key}, Value{kvp.Value}\n");
            }

            return sb.ToString();
        }

        public List<CellModel> GetComposition()
        {
            return this.parts.ToList(); 
        }

        public void ChangeOriginCoordinates(Vector2I newCoordinates)
        {
            for (int offset = 0; offset < this.GetPartsNumber(); offset++)
            {
                if (this.GetDirection() == Direction2D.HORIZONTAL)
                {
                    this.parts[offset].SetPlacement(newCoordinates.X + offset, 
                        newCoordinates.Y);
                }
                else if (this.GetDirection() == Direction2D.VERTICAL)
                {
                    this.parts[offset].SetPlacement(newCoordinates.X,
                        newCoordinates.Y + offset);
                }
            }
        }
    }
}