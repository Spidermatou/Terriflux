using Godot;
using System;
using System.ComponentModel;
using Terriflux.Programs.Controller;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
	public partial class RoundCounter : Node2D, IRoundObserver
	{
		private Label _roundNumberLabel;

		// CONSTRUCT	
		private RoundCounter() { }

		public static RoundCounter Design()
		{
			return (RoundCounter)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "RoundCounter" + OurPaths.NODEXT)
				.Instantiate();
		}

		public override void _Ready()
		{

            _roundNumberLabel = GetNode<Label>("Count");

		}

		public void Update(int roundNumber)
		{
            _roundNumberLabel.Text = roundNumber.ToString();
		}

		public void OnNextTurnPressed()
		{
			RoundController.NextTurn();
		}

    }
}
