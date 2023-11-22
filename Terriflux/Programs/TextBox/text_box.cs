using Godot;
using System;
namespace Terriflux.Programs.GameContext.OurPath
{
	/// <summary>
	/// Ne peut être ferme ! Une fois le joueur devant cette boite de dialogue de fin de jeu, il devra 
	/// appuyer sur le bouton "relancer / quitter le jeu". 
	/// </summary>
	public partial class text_box : CanvasLayer
	{
		private Label _message;

		/// <summary>
		/// A eviter !! Utiliser la fonction static Design() ! 
		/// </summary>
		public text_box() { }

		public override void _Ready()
		{
			_message = GetNode<Label>("MarginContainer/MarginContainer/HBoxContainer/Label");
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
	}
}



