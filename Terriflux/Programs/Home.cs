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
        // test
        Grid test = GridFactory.CreateNoMansLand(10);
        TerritoryView tw = TerritoryView.Create();
        this.AddChild(tw);
        tw.UpdateMap(test);
    }

	public override void _Process(double delta)
	{
	}
}
