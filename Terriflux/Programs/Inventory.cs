using Godot;
using System;
using Terriflux.Programs;

public partial class Inventory : RawNode, IInventory        // TODO
{
    public void Add(FlowKind flow, int amount)
    {
        throw new NotImplementedException();
    }

    public bool Contains(FlowKind flow)
    {
        throw new NotImplementedException();
    }

    public int GetQuantityOf(FlowKind flow)
    {
        throw new NotImplementedException();
    }

    public void Remove(FlowKind flow, int amount)
    {
        throw new NotImplementedException();
    }

    public override void _Ready()
	{
		base._Ready();
	}

}
