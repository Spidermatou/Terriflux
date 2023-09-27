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
        Grid test = GridFactory.CreateNoMansLand(10);
        TerritoryView tw = TerritoryView.Create();
        this.AddChild(tw);
        tw.UpdateMap(test);
        test.SetObserver(tw);

        // test building
        BuildingModel bm = new BuildingModel("field", ".png", 5, 5);
        GD.Print(bm.GetCellName());
        /*
        BuildingView bv = BuildingView.Create(bm.GetCellName(),
            Paths.TEXTURES+bm.GetCellName()+bm.GetSkinExtension(),
            bm.GetPlacement().X,
            bm.GetPlacement().Y);
        test.SetAt(bm,
            bm.GetPlacement().X,
            bm.GetPlacement().Y); */

    }

    public override void _Process(double delta)
	{
	}
}
