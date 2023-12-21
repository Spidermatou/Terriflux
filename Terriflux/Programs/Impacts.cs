using Godot;
using System;
using Terriflux.Programs;

public partial class Impacts : RawNode, IImpacts
{
    private Gauge _social;
    private Gauge _economy;
    private Gauge _ecology;

    public override void _Ready()
    {
        base._Ready();

        _social = GetNode<Gauge>("Social");
        _economy = GetNode<Gauge>("Economy");
        _ecology = GetNode<Gauge>("Ecology");
    }

    public double GetEcology()
    {
        return _ecology.Get();
    }

    public double GetEconomy()
    {
        return _economy.Get();
    }

    public double GetSociability()
    {
        return _social.Get();
    }

    public void IncrementsEcology(double add)
    {
        _ecology.Increments(add);
        GD.Print($"access ecol {add}");

    }

    public void IncrementsEconomy(double add)
    {
        _economy.Increments(add);
        GD.Print($"access econ {add}");


    }

    public void IncrementsSocial(double add)
    {
        GD.Print($"access soc {add}");

        _social.Increments(add);
    }
}
