using Godot;
using System;

namespace Terriflux.Programs.GameContext.OurPath
{
	public partial class text_box_event : CanvasLayer
	{
		private Label _nom;
		private Label _message;
		private Sprite2D _photo;

		public override void _Ready()
		{
				_message = GetNode<Label>("/MarginContainer/MarginContainer/MarginContainer/HBoxContainer/Message");
				_nom = GetNode<Label>("/MarginContainer/MarginContainer/Nom");
				_photo = GetNode<Sprite2D>("/MarginContainer/MarginContainer/Photo");
		}	

		public override void _Process(double delta)
		{
		}
		public void setNom(String Nom)
		{
			_nom.Text = Nom;
		}
		public void setMessage(String Message)
		{
			_message.Text = Message;
		}
		public  text_box_event Design()
		{
			return (text_box_event)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "text_box_event.tscn")
				.Instantiate();
		}
		private void _on_texture_button_pressed()
		{
			this.GetTree().Quit();
		}
	}
}



