using Godot;
using System;
using System.ComponentModel;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Observers;

public partial class RoundCounter : Node2D, IRoundObserver
{
	private Label _label;

	// CONSTRUCT	
	private RoundCounter() { }
	public static RoundCounter Design() 
	{
		return (RoundCounter)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "RoundCounter" + OurPaths.NODEXT)
			.Instantiate();
	}

    public override void _Ready()
	{

		_label = GetNode<Label>("Count");

    }

    public void Update(int roundNumber)
    {
        _label.Text = roundNumber.ToString();
    }
}
