using Godot;
using System;

namespace Terriflux.Programs.View
{
	public partial class RoundCounter : Node2D
	{
        private Label _label;

		public override void _Ready()
		{

			_label = GetNode<Label>("Count");

		}

		public void Update(int roundNumber)
        {
            _label.Text = roundNumber.ToString();
        }
	}
}