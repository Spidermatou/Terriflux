using Godot;
using System;

interface ICellObservable 
{
    void AddObserver(ICellObserver observer);
    void RemoveObserver(ICellObserver observer);
    /// <summary>
    /// Force all notifies call for cells informations
    /// </summary>
    void NotifyAllCellsInfos();
    void NotifyPlacement();
    void NotifyCellName();
    void NotifyKind();


}
