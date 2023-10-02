using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// Represents a building
    /// </summary>
    public partial class BuildingModel : IBuildingObservable
    {
        private static readonly Dictionary<InfluenceScale, int> MULTIPLICATION_RATE = new()
        {
            { InfluenceScale.NATIONAL, 1 },
            { InfluenceScale.NATIONAL, 3 },
            { InfluenceScale.WORLDWIDE, 5 },
  
        }; // the larger the scale, the larger the needs and production will be 

        private readonly List<IBuildingObserver> observers = new();
        private int[] impacts = new int[3];
        private Dictionary<FlowKind, int> needs = new();
        private Dictionary<FlowKind, int> products = new();
        private readonly List<CellModel> parts = new();      // cells wich compose the building
        private InfluenceScale actualInfluenceScale;
        private string name = "Building";       

        
        public BuildingModel(string name, 
            int size, 
            InfluenceScale influence,
            Dictionary<FlowKind, int> needs, 
            Dictionary<FlowKind, int> products)
        {
            // build attributes
            SetInfluence(influence);
            SetBasisNeeds(needs);
            SetBasisProduction(products);

            // build the building with cells
            for (int i = 0; i < size; i++)
            {
                CellModel part = new();
                part.SetCellName(name);
                part.SetCellKind(CellKind.BUILDING);
                this.parts.Add(part);
            }
        }

        // Building's PRIMARY INFORMATIONS
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
        public int GetSize()
        {
            return this.parts.Count;
        }

        // Building's PRODUCTION
        /// <summary>
        /// Modify building productions, then apply influence scale's modificators
        /// </summary>
        /// <param name="products">Default building productions, at lowest scale of influence</param>
        public void SetBasisProduction(IDictionary<FlowKind, int> products) 
        {
            this.products = (Dictionary<FlowKind, int>)products;

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
                // clone
                observer.UpdateProducts(this.products.ToDictionary(entry => entry.Key,
                                                                  entry => entry.Value));
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
        public void SetBasisNeeds(IDictionary<FlowKind, int> needs) 
        {
            this.needs = (Dictionary<FlowKind, int>)needs;

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
                // clone
                observer.UpdateNeeds(this.needs.ToDictionary(entry => entry.Key,
                                                                  entry => entry.Value));
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
                observer.UpdateInfluence(this.actualInfluenceScale);
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
                observer.UpdateImpacts(impacts);
            }
        }

        // ALLS
        public void NotifyAlls()
        {
            // cells infos
            foreach (CellModel part in parts)
            {
                part.NotifyAlls();
            }

            // builds infos
            NotifyImpacts();
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
        public void AddCompositionObserver(ICollection<ICellObserver> observers)
        {
            // security
            if (observers.Count != this.GetSize())
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
        public void RemoveCompositionObserver(ICollection<ICellObserver> observers)
        {
            // security
            if (observers.Count != this.GetSize())
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
    }
}