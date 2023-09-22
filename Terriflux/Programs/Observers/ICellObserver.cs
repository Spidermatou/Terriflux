using Godot;
using System;

public interface ICellObserver 
{
    public void UpdatePlacement(Vector2I coordinates);
    public void UpdateCellName(string cname);
}
