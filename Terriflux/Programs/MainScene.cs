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
		alert = (Alert) RawNode.Instantiate("Alert");
		this.AddChild(alert);

		alert.Say("pomme");
    }
}
