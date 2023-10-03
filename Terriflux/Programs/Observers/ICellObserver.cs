using Godot;
using System;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.Observers
{
    public interface ICellObserver
    {
        void UpdateCellName(string name);

        void UpdatePlacement(Vector2 coordinates);

        void UpdateCellKind(CellKind kind);
    }
}