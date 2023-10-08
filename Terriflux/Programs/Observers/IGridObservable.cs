using Godot;
using System;

namespace Terriflux.Programs.Observers
{
    public interface IGridObservable
    {
        public void AddObserver(IGridObserver observer);
        public void NotifyGridChanges();
    }
}