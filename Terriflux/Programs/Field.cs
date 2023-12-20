using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;
public partial class Field : Building
{
    public Field() : base(
        new double[] { 1.5, 4.0, -2.0 },
        new Dictionary<FlowKind, int> { { FlowKind.Water, 1 } },
        new Dictionary<FlowKind, int> { { FlowKind.Cereal, 3 } },
        new("#Ce517b")
        )
    { }

    public override void _Ready()
    {
        base._Ready();

        this._buildingSprite.Scale = new((float)1.05, (float)1.05);
    }
}
