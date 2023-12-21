using Godot;
using System;
using Terriflux.Programs;

public partial class EconomyGauge : Gauge
{
    public EconomyGauge() : base() { }

    public override void _Ready()
    {
        base._Ready();

        ChangeBarDescription("Economie");
    }
}
