using Godot;
using System;
using Terriflux.Programs;

public partial class SocialGauge : Gauge
{
    public SocialGauge() : base() { }

    public override void _Ready()
    {
        base._Ready();

        ChangeBarDescription("Social");
    }
}
