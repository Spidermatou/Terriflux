using Godot;
using System;

public interface ICellObserver 
{
    public void UpdatePlacement(Vector2 coordinates);
    public void UpdateCellName(StringName cname);
}
