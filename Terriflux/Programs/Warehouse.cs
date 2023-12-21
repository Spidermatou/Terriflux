using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;
public partial class Warehouse : Building
{
    private IInventory inventory;
    private readonly List<Building> neighbour; // list of elements with access to the warehouse
    public const int EFFECT_ZONE_SIZE = 3;

    public Warehouse() : base(
        new double[] { -1, -1, -1 },
        new Dictionary<FlowKind, int> { },
        new Dictionary<FlowKind, int> { },
        new("#B51405")
        )
    { 
        this.neighbour = new();  
    }

    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)4.45, (float)4.45);
    }

    public List<Building> GetVoisins()
    {
        return this.neighbour;
    }

    public void AddNeighbour(Building neighbour)
    {
        this.neighbour.Add(neighbour);
        foreach (Building act_neighbour in this.neighbour)
        {
            GD.Print(act_neighbour.Verbose());
        }
    }

    public IInventory GetInventory()
    {
        return this.inventory;
    }

    public void SetInventory(IInventory inventory)
    {
        this.inventory = inventory;
        // inventory.GetWarehouses().Add(this); // TT
    }

}
