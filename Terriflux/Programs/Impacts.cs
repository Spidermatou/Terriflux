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
        VerifyFail();
    }

    public void IncrementsEconomy(double add)
    {
        _economy.Increments(add);
        VerifyFail();
    }

    public void IncrementsSocial(double add)
    {
        _social.Increments(add);
        VerifyFail();
    }

    /// <summary>
    /// Checks whether one of the bars has fallen below the critical threshold (0 and below), 
    /// and therefore whether the player has lost or not.
    /// </summary>
    private void VerifyFail()
    {
        if (GetSociability() <= 0 || GetEcology() <= 0 || GetEconomy() <= 0)
        {
            End.Fail();
        }
    }
}
