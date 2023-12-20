using Godot;
using System;
using Terriflux.Programs;

public partial class MainScene : RawNode
{
	public MainScene() : base() { }

	public override void _Ready()
	{
		base._Ready();

        // create grid      (have to be generate via script)
        GridBuilder gridBuilder = new();
        gridBuilder.BuildWasteland(new(7, 7));
        Grid grid = gridBuilder.GetResult();
        grid.Position = GetNode<Marker2D>("GridMark").Position;
        grid.Scale = new Vector2((float)0.9, (float)0.9); 
        this.AddChild(grid);

        // get placement list
        PlacementList placementList = GetNode<PlacementList>("PlacementList");
    }

    private void OnExitGamePressed()
    {
        GetTree().Quit();
    }
}
