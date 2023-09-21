using Godot;
using System;

public interface ICellObservable 
{
    public void SetObserver(ICellObserver observer);
    public void NotifyPlacement();
    public void NotifyCellName();
}
