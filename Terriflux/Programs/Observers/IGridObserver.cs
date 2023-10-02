using Godot;
using System;

namespace Terriflux.Programs.Observers
{
    public interface IGridObserver
    {
        public void UpdateMap(Grid grid);
    }
}