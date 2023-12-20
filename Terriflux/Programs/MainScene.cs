using Godot;
using System;
using Terriflux.Programs;

public partial class MainScene : RawNode
{
	public MainScene() : base() { }

	public override void _Ready()
	{
		base._Ready();

        // create grid
        GridBuilder gridBuilder = new();
        gridBuilder.BuildWasteland(new(7, 7));
        Grid grid = gridBuilder.GetResult();
        grid.Position = GetNode<Marker2D>("GridMark").Position;
        grid.Scale = new Vector2((float)0.9, (float)0.9); 
        this.AddChild(grid);    

        // create placement list
        PlacementList placementList = (PlacementList) Instantiate("PlacementList");
        placementList.Position = GetNode<Marker2D>("ListMark").Position;
        this.AddChild(placementList);
    }
}
