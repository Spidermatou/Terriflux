using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Terriflux.Programs.Observers;

public abstract partial class ABuildingModel : CellModel, IBuildingObservable
{
    private IBuildingObserver observer;
    protected int[] impacts = new int[3];
    protected Dictionary<Flow, int> needs = new Dictionary<Flow, int>();
    protected Dictionary<Flow, int> products = new Dictionary<Flow, int>();

    private ABuildingModel() { }

    public int[] GetImpacts()
    {
        return impacts; 
    }

    public Flow[] GetFlowNeeds()
    {
        return this.needs.Keys.ToArray();
    }

    public Flow[] GetFlowProducts()
    {
        return this.products.Keys.ToArray();
    }

    public int GetQuantityNeeded(Flow kind)
    {
        return this.needs[kind];
    }

    public int GetQuantityProduced(Flow kind)
    {
        return this.needs[kind];
    }

    public void SetObserver(IBuildingObserver observer)
    {
        this.observer = observer;   
    }

    public void NotifyImpacts()
    {
        this.observer.UpdateImpacts();
    }
}
