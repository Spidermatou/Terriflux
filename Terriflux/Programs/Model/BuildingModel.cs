using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terriflux.Programs.Observers;

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
        Dictionary<FlowKind, int> needs, Dictionary<FlowKind, int> products) {
        this.SetCellName(name.Capitalize());
        this.SetSkinExtension("png");
        this.SetOccupation(occupation); 
        this.SetPlacement(x, y);
        this.SetInfluenceScale(influence); 

        // needs
        foreach (KeyValuePair<FlowKind, int> kvp in needs){
            this.AddNeed(kvp.Key, kvp.Value * MULTIPLICATION_RATE[influence]);
        }

        // needs
        foreach (KeyValuePair<FlowKind, int> kvp in products)
        {
            this.AddProduct(kvp.Key, kvp.Value * MULTIPLICATION_RATE[influence]);
        }
    }

    public void AddProduct(FlowKind flow, int quantity)
    {
        if (!this.products.ContainsKey(flow))
        {
            this.products.Add(flow, quantity);
            NotifyProducts();
        }
    }

    public void NotifyProducts()
    {
        foreach (IBuildingObserver observer in this.buildingObservers)
        {
            // clone
            observer.UpdateProducts(this.products.ToDictionary(entry => entry.Key,
                                                              entry => entry.Value));
        }
    }

    public void AddNeed(FlowKind flow, int quantity)
    {
        if (!this.needs.ContainsKey(flow))
        {
            this.needs.Add(flow, quantity);
            NotifyNeeds();
        }
    }

    public void NotifyNeeds()
    {
        foreach (IBuildingObserver observer in this.buildingObservers)
        {
            // clone
            observer.UpdateProducts(this.needs.ToDictionary(entry => entry.Key,
                                                              entry => entry.Value));
        }
    }

    public void SetInfluenceScale (InfluenceScale influence)
    {
        this.actualInfluenceScale = influence;
        NotifyInfluenceScale();
    }

    public void NotifyInfluenceScale()
    {
        foreach (IBuildingObserver observer in this.buildingObservers)
        {
            observer.UpdateInfluenceScale(this.actualInfluenceScale);
        }
    }

    public void SetOccupation(int occupation)
    {
        this.occupation = occupation;
        NotifyOccupation();
    }

    public void NotifyOccupation()
    {
        foreach (IBuildingObserver observer in this.buildingObservers)
        {
            // clone
            observer.UpdateOccupation(this.occupation);
        }
    }

    public int GetOccupation()
    {
        return this.occupation;
    }

    public int[] GetImpacts()
    {
        return impacts; 
    }

    public FlowKind[] GetFlowNeeds()
    {
        return this.needs.Keys.ToArray();
    }

    public FlowKind[] GetFlowProducts()
    {
        return this.products.Keys.ToArray();
    }

    public int GetQuantityNeeded(FlowKind kind)
    {
        return this.needs[kind];
    }

    public int GetQuantityProduced(FlowKind kind)
    {
        return this.needs[kind];
    }

    public InfluenceScale GetInfluence()
    {
        return this.actualInfluenceScale;
    }

    public void RegisterBuildingObserver(IBuildingObserver observer)
    {
        if (!this.buildingObservers.Contains(observer))
        {
            this.buildingObservers.Add(observer);
        }
    }

    public void UnregisterBuildingObserver(IBuildingObserver observer)
    {
        if (this.buildingObservers.Contains(observer))
        {
            this.buildingObservers.Remove(observer);
        }
    }

    public void NotifyImpacts()
    {
        foreach (IBuildingObserver observer in this.buildingObservers)
        {
            observer.UpdateImpacts(this.impacts);
        }
    }
}
