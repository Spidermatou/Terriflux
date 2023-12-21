using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terriflux.Programs
{
    public interface IGauge
    {
        void Increments(double add);
        double Get();
    }
}