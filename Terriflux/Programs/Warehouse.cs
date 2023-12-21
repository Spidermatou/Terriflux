using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;
public partial class Warehouse : Building
{
    private IInventory inventory;
    private readonly List<Building> neighbours; // list of elements with access to the warehouse
    public const int EFFECT_ZONE_SIZE = 3;

    public Warehouse() : base(
        new double[] { -1, -1, -1 },
        new Dictionary<FlowKind, int> { },
        new Dictionary<FlowKind, int> { },
        new("#B51405")
        )
    { 
        this.neighbours = new();  
    }

    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)4.45, (float)4.45);
    }

    public List<Building> GetNeighbours()
    {
        return this.neighbours;
    }

    public bool HasNeighbour(Building building)
    {
        return neighbours.Contains(building);
    }

    public void AddNeighbour(Building neighbour)
    {
        this.neighbours.Add(neighbour);
    }

    public void RemoveNeighbour(Building neighbour)
    {
        if (HasNeighbour(neighbour))
        {
            this.neighbours.Remove(neighbour);
        }
    }

    public IInventory GetInventory()
    {
        return this.inventory;
    }

    public void SetInventory(IInventory inventory)
    {
        this.inventory = inventory;
    }

}
