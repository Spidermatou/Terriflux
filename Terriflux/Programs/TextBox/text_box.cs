using Godot;
using System;
namespace Terriflux.Programs.GameContext.OurPath
{
	public partial class text_box : CanvasLayer
	{
	
		private Label _message;
		public override void _Ready()
		{
			_message = GetNode<Label>("MarginContainer/MarginContainer/HBoxContainer/Label");

			if (_message == null) throw new Exception("message bug");
		}
		
		public void SetMessage(String Message)
		{
			_message.Text = Message;
		}
		
		public static text_box Design()
		{
			return (text_box)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "text_box.tscn")
				.Instantiate();
		}
		private void _on_texture_button_pressed()
		{
			this.QueueFree();
		}
	}
}



