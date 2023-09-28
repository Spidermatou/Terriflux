using Godot;
using System;

public interface ICellObservable 
{
    public void AddObserver(ICellObserver observer);
    public void RemoveObserver(ICellObserver observer);
    public void NotifyPlacement();
    public void NotifyCellName();
    public void NotifyKind();
}
