using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Terriflux.Programs.Observers;

public partial class BuildingModel : CellModel, IBuildingObservable
{
    private IBuildingObserver observer;
    protected int[] impacts = new int[3];
    protected Dictionary<FlowKind, int> needs = new Dictionary<FlowKind, int>();
    protected Dictionary<FlowKind, int> products = new Dictionary<FlowKind, int>();

    public BuildingModel(string name, string textureExtension) {
        this.SetCellName(name.Capitalize());
        this.SetSkinExtension(textureExtension);

    }
    public BuildingModel(string name, string textureExtension, int x, int y) {
        this.SetCellName(name.Capitalize());
        this.SetSkinExtension(textureExtension);
        this.SetPlacement(x, y);
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

    public void SetObserver(IBuildingObserver observer)
    {
        this.observer = observer;   
    }

    public void NotifyImpacts()
    {
        this.observer.UpdateImpacts();
    }
}
