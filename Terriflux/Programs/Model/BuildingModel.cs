using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{
    public partial class BuildingModel : CellModel, IBuildingObservable
    {
        private static readonly Dictionary<InfluenceScale, int> MULTIPLICATION_RATE = new Dictionary<InfluenceScale, int>()
        {
            { InfluenceScale.NATIONAL, 1 },
            { InfluenceScale.NATIONAL, 3 },
            { InfluenceScale.WORLDWIDE, 5 },
        }; // the larger the scale, the larger the needs and production will be 


        private List<IBuildingObserver> buildingObservers = new List<IBuildingObserver>();
        private int[] impacts = new int[3];
        private Dictionary<FlowKind, int> needs = new Dictionary<FlowKind, int>();
        private Dictionary<FlowKind, int> products = new Dictionary<FlowKind, int>();
        private int occupation; //  number of cells occupied by the building in length or width
        private InfluenceScale actualInfluenceScale;

        public BuildingModel(string name, int occupation, int x, int y, InfluenceScale influence,
            Dictionary<FlowKind, int> needs, Dictionary<FlowKind, int> products)
        {
            SetCellName(name.Capitalize());
            SetSkinExtension("png");
            SetOccupation(occupation);
            SetPlacement(x, y);
            SetInfluenceScale(influence);

            // needs
            foreach (KeyValuePair<FlowKind, int> kvp in needs)
            {
                AddNeed(kvp.Key, kvp.Value * MULTIPLICATION_RATE[influence]);
            }

            // needs
            foreach (KeyValuePair<FlowKind, int> kvp in products)
            {
                AddProduct(kvp.Key, kvp.Value * MULTIPLICATION_RATE[influence]);
            }
        }

        public void AddProduct(FlowKind flow, int quantity)
        {
            if (!products.ContainsKey(flow))
            {
                products.Add(flow, quantity);
                NotifyProducts();
            }
        }

        public void NotifyProducts()
        {
            foreach (IBuildingObserver observer in buildingObservers)
            {
                // clone
                observer.UpdateProducts(products.ToDictionary(entry => entry.Key,
                                                                  entry => entry.Value));
            }
        }

        public void AddNeed(FlowKind flow, int quantity)
        {
            if (!needs.ContainsKey(flow))
            {
                needs.Add(flow, quantity);
                NotifyNeeds();
            }
        }

        public void NotifyNeeds()
        {
            foreach (IBuildingObserver observer in buildingObservers)
            {
                // clone
                observer.UpdateProducts(needs.ToDictionary(entry => entry.Key,
                                                                  entry => entry.Value));
            }
        }

        public void SetInfluenceScale(InfluenceScale influence)
        {
            actualInfluenceScale = influence;
            NotifyInfluenceScale();
        }

        public void NotifyInfluenceScale()
        {
            foreach (IBuildingObserver observer in buildingObservers)
            {
                observer.UpdateInfluenceScale(actualInfluenceScale);
            }
        }

        public void SetOccupation(int occupation)
        {
            this.occupation = occupation;
            NotifyOccupation();
        }

        public void NotifyOccupation()
        {
            foreach (IBuildingObserver observer in buildingObservers)
            {
                // clone
                observer.UpdateOccupation(occupation);
            }
        }

        public int GetOccupation()
        {
            return occupation;
        }

        public int[] GetImpacts()
        {
            return impacts;
        }

        public FlowKind[] GetFlowNeeds()
        {
            return needs.Keys.ToArray();
        }

        public FlowKind[] GetFlowProducts()
        {
            return products.Keys.ToArray();
        }

        public int GetQuantityNeeded(FlowKind kind)
        {
            return needs[kind];
        }

        public int GetQuantityProduced(FlowKind kind)
        {
            return needs[kind];
        }

        public InfluenceScale GetInfluence()
        {
            return actualInfluenceScale;
        }

        public void RegisterBuildingObserver(IBuildingObserver observer)
        {
            if (!buildingObservers.Contains(observer))
            {
                buildingObservers.Add(observer);
            }
        }

        public void UnregisterBuildingObserver(IBuildingObserver observer)
        {
            if (buildingObservers.Contains(observer))
            {
                buildingObservers.Remove(observer);
            }
        }

        public void NotifyImpacts()
        {
            foreach (IBuildingObserver observer in buildingObservers)
            {
                observer.UpdateImpacts(impacts);
            }
        }

        public void NotifyAllBuildingInfos()
        {
            // cells infos
            NotifyAllCellsInfos();

            // builds infos
            NotifyImpacts();
            NotifyInfluenceScale();
            NotifyNeeds();
            NotifyProducts();
            NotifyOccupation();
        }
    }
}