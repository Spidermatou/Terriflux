using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;

/// <summary>
/// A Factory building (not the design pattern).
/// </summary>
public partial class Grocery : Building
{
    // /' 15.0, -2.5, -2.5 ; MANUFACTURED_MERCHANDISE,3, ENERGY,1 ; #51ceb4 '/
    public Grocery() : base(
        new double[] { 3.0, -1.0, -4.0 },
        new Dictionary<FlowKind, int> { { FlowKind.Energy, 1 }, { FlowKind.Composant, 2 } },
        new Dictionary<FlowKind, int> { { FlowKind.Machine, 3 } },
        new("#0684A9")
        )
    { }


    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)4.30, (float)4.30);
    }
}
