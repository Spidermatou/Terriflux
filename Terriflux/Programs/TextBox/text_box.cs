using Godot;
using System;
namespace Terriflux.Programs.GameContext.OurPath
{
	public partial class text_box : CanvasLayer
	{
	
			// Called when the node enters the scene tree for the first time.
		private Label _message;
		public override void _Ready()
		{
			_message = GetNode<Label>("/MarginContainer/MarginContainer/HBoxContainer/Label");
		}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		
		}
		
		public void setMessage(String Message)
		{
			_message.Text = Message;
		}
		
		public  text_box Design()
		{
			return (text_box)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "text_box.tscn")
				.Instantiate();
		}
		private void _on_texture_button_pressed()
	{
		
		this.quit;
	// Replace with function body.
		
		}
	}
}



