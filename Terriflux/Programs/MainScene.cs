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


		/////////////// TESTS
		//alert.Say("pomme");
		Field f = (Field)Instantiate("Field");
		f.Position = new Vector2((float)0.0, (float)0.0);
        this.AddChild(f);

        Bakery b = (Bakery)Instantiate("Bakery");
        b.Position = new Vector2((float)100.0, (float)100.0);
        this.AddChild(b);



    }
}
