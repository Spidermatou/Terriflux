using Godot;
using System;
using Terriflux.Programs.Observers;

interface ICellObservable 
{
    void AddObserver(ICellObserver observer);

    void RemoveObserver(ICellObserver observer);

    /// <summary>
    /// Force all notifies call for cells informations
    /// </summary>
    void NotifyAlls();

    void NotifyPlacement();

    void NotifyCellName();

    void NotifyKind();
}
