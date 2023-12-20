using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;
public partial class Warehouse : Building
{
    public Warehouse() : base(
        new double[] { -1, -1, -1 },
        new Dictionary<FlowKind, int> { },
        new Dictionary<FlowKind, int> { },
        new("#B51405")
        )
    { }

    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)4.45, (float)4.45);
    }
}
