using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;

/// <summary>
/// A Factory building (not the design pattern).
/// </summary>
public partial class Shaft : Building
{
    public Shaft() : base(
        new double[] { 3.0, -1.0, -4.0 },
        new Dictionary<FlowKind, int> { },
        new Dictionary<FlowKind, int> { { FlowKind.Water, 3 } },
        new("#0684A9")
        )
    { }

    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)4.30, (float)4.30);
    }
}
