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
		this.AddChild((Field)Instantiate("Field"));

		

    }
}
