using Godot;
using System;

namespace Terriflux.Programs;

/// <summary>
/// Supposed abstract.
/// </summary>
public partial class Gauge : RawNode, IGauge
{
    protected TextureProgressBar _bar;

    protected Gauge() : base() { }

    public override void _Ready()
    {
        base._Ready();

        _bar = GetNode<TextureProgressBar>("Bar");

        ChangeBarSkin(GD.Load<Texture2D>(PATH_IMAGES + "complete_" + GetType().Name.ToLower() + ".png"));

        // default value
        _bar.Value = 50;
    }

    public void Increments(double add)
    {
        _bar.Value += add;
    }

    public double Get()
    {
        return _bar.Value;
    }

    protected void ChangeBarSkin(Texture2D bar)
    {
        _bar.TextureProgress = bar;
    }

    protected void ChangeBarDescription(string description)
    {
        GetNode<Label>("Description").Text= description;
    }
}
