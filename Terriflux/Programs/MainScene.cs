using Godot;
using System;
using Terriflux.Programs;

public partial class MainScene : RawNode
{
	private Alert alert;

	public MainScene() : base() { }

	public override void _Ready()
	{
		base._Ready();

		// create alert
		alert = (Alert) Instantiate("Alert");
		this.AddChild(alert);

        // create grid
        GridBuilder gridBuilder = new();
        gridBuilder.BuildWasteland(new(7, 7));
        Grid grid = gridBuilder.GetResult();
        grid.Position = GetNode<Marker2D>("GridMark").Position;
        grid.Scale = new Vector2((float)0.9, (float)0.9); 
        this.AddChild(grid);    

        // create placement list
        PlacementList placementList = (PlacementList) Instantiate("placementList");
        placementList.Position = GetNode<Marker2D>("ListMark").Position;
        this.AddChild(placementList);

    }
}
