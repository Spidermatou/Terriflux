using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Terriflux.Programs.GameContext;

public partial class BuildingView : CellView, IBuildingObserver
{

    private BuildingView() : base() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildingName"></param>
    /// <returns>The created BuildingView node, with basic cell skin </returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static BuildingView Create(string buildingName)
	{
        string texturePath = Paths.TEXTURES + buildingName.ToLower() + Paths.PNGEXT;

        if (File.Exists(texturePath))
		{
            BuildingView building = (BuildingView)GD.Load<PackedScene>(Paths.VIEW_NODES + "BuildingView" + Paths.GDEXT)
				.Instantiate();
			return building;
        }
		else
		{
            throw new FileNotFoundException();
		}
    }


    public override void _Ready()
	{
		GD.Print("Building created!"); // test
	}

	public override void _Process(double delta)
	{
	}

    public void UpdateImpacts() 
    {
		// Nothing
    }
}
