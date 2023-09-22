using Godot;
using System;

public interface IGridObservable
{
    public void SetObserver(IGridObserver observer);
    public void Notify();
}
