using Godot;
using System;
using Terriflux.Programs.GameContext;

/// <summary>
/// Start the game
/// </summary>
public partial class Home : Node2D
{
	public override void _Ready()
	{
        // test grid
        Grid grid_test = GridFactory.CreateFullGrassLand(10);
        TerritoryView territory_view = TerritoryView.Create();
        this.AddChild(territory_view);
        territory_view.UpdateMap(grid_test);
        grid_test.SetObserver(territory_view);

        // test building
        // todo


    }

    public override void _Process(double delta)
	{
	}
}
