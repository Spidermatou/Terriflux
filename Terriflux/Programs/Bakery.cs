using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;

/// <summary>
/// A Factory building (not the design pattern).
/// </summary>
public partial class Bakery : Building
{
    //  /'  ; WATER,2, ENERGY,1,  ; BREAD,3 ; #Ceb851 '/
    public Bakery() : base(
        new double[] { 3.0, -1.0, -4.0 },
        new Dictionary<FlowKind, int> { { FlowKind.Water, 2 }, { FlowKind.Cereal, 2 } },
        new Dictionary<FlowKind, int> { { FlowKind.Bread, 3 } },
        new("#F15309")
        )
    { }

    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)4.30, (float)4.30);
    }
}
