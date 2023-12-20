using Godot;
using System.Collections.Generic;
using System;

namespace Terriflux.Programs;
public partial class Warehouse : Building
{
    public Warehouse() : base(
        new double[] { -1, -1, -1 },
        new Dictionary<FlowKind, int> { },
        new Dictionary<FlowKind, int> { },
        new("#B51405")
        )
    { }
}
