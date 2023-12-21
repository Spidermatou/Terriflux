using Godot;
using System;
using Terriflux.Programs;

public partial class EcologyGauge : Gauge
{
	public EcologyGauge() : base() { }

    public override void _Ready()
    {
        base._Ready();

        ChangeBarDescription("Ecologie");
    }
}
