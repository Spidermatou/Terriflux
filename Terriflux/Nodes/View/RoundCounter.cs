using Godot;
using System;

namespace Terriflux.Programs.View
{
	public partial class RoundCounter : Node2D
	{
		private int roundCount = 1;

		private Label _label;

		public override void _Ready()
		{

			_label = GetNode<Label>("Count");

		}

		public override void _Process(double delta)
		{
			_label.Text = this.roundCount.ToString();
		}
	}
}