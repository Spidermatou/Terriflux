using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terriflux.Programs
{
    public interface IInventory
    {
        void Add(FlowKind flow, int amount);
        void Remove(FlowKind flow, int amount);
        bool Contains(FlowKind flow);
        int GetQuantityOf(FlowKind flow);
        bool TryImportExport();
    }
}