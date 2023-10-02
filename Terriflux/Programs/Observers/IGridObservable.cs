using Godot;
using System;

namespace Terriflux.Programs.Observers
{
    public interface IGridObservable
    {
        public void SetObserver(IGridObserver observer);
        public void Notify();
    }
}