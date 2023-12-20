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


        /////////////// TESTS
        //alert.Say("pomme");
        /*Field f = (Field)Instantiate("Field");
		f.Position = new Vector2((float)0.0, (float)0.0);
        this.AddChild(f);

        Bakery b = (Bakery)Instantiate("Bakery");
        b.Position = new Vector2((float)100.0, (float)100.0);
        this.AddChild(b); */

        GD.Print(this.GetChildren().Count);

    
    }
}
