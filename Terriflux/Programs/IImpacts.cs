using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terriflux.Programs
{
    public interface IImpacts
    {
        void IncrementsEcology(double add);
        void IncrementsEconomy(double add);
        void IncrementsSocial(double add);

        double GetEcology();
        double GetEconomy();
        double GetSociability();
    }
}