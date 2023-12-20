using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;

/// <summary>
/// A Factory building (not the design pattern).
/// </summary>
public partial class Factory : Building
{

    public Factory() : base(
        new double[] { 8.5, -23.0, -11.8 },
        new Dictionary<FlowKind, int> { { FlowKind.Energy, 2 }, {FlowKind.Composant, 2 } },
        new Dictionary<FlowKind, int> { { FlowKind.Machine, 3 } },
        new("#A7A197")
        )
    { }

    public override void _Ready()
    {
        base._Ready();
        this._buildingSprite.Scale = new Vector2((float)3.5, (float)3.5);
    }
}
