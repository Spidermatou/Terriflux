using Godot;
using System;
namespace Terriflux.Programs.GameContext.OurPath
{
public partial class text_box : CanvasLayer
{
	private Label _message;
	private Label _start;
	private Label _end;
	// Called when the node enters the scene tree for the first time.
	protected text_box()
	{
		
	}
	public override void _Ready()
	{
		_message = GetNode<Label>("MarginContainer/MarginContainer/HBoxContainer/Label");
		_start = GetNode<Label>("MarginContainer/MarginContainer/HBoxContainer/StartMessage");
		_end = GetNode<Label>("MarginContainer/MarginContainer/HBoxContainer/EndMessage");
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public void setMessage(String text)
	{
		_message.Text=text;
	}
	
	public static text_box Design()
	{
		return (text_box)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "text_box.tscn")
				.Instantiate();		
	}
	private void _on_texture_button_pressed()
	{
	// Replace with function body.
		this.QueueFree();
	}
}
}



