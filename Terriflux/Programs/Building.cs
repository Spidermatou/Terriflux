using Godot;
using System;
using Terriflux.Programs;

/// <summary>
/// Abstract node
/// </summary>
public partial class Building : Cell
{
    // children
	private Sprite2D _buildingSprite;

    public Building(Texture2D texture) 
    {
        this._buildingSprite.Texture = texture;    
    }


    public override void _Ready()
    {
        base._Ready();
        _buildingSprite = GetNode<Sprite2D>("BuildingSprite");
    }




}
