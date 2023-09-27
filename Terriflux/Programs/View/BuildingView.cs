using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using Terriflux.Programs.GameContext;

public partial class BuildingView : CellView, IBuildingObserver
{
	private BuildingView() : base() { } 


	public static BuildingView Create(string buildingName, string texturePath, int x, int y)
	{
		if (!File.Exists(Paths.VIEW_NODES + buildingName.ToLower() + Paths.GDEXT))
		{
            BuildingView node = (BuildingView)GD.Load<PackedScene>(Paths.VIEW_NODES + buildingName.Capitalize() + Paths.GDEXT)
				.Instantiate();
			node.ChangeSkin(texturePath);
			return node;
		}
		else
		{
			throw new FileNotFoundException();
		}
    }


	public override void _Ready()
	{
		//
		GD.Print("Building created!");
	}

	public override void _Process(double delta)
	{
	}

    public void UpdateImpacts() // TODO
    {
        throw new NotImplementedException();
    }
}
