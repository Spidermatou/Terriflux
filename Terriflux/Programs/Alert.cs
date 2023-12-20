using Godot;
using System;

namespace Terriflux.Programs;
public partial class Alert : RawNode, IAlert
{
	Label _message;

    public Alert() : base() { }

    public override void _Ready()
	{
		base._Ready();
		this._message = GetNode<Label>("Message");
		Close();
    }

    public void Say(string message)
	{
		ModifyMessage(message);
		Show();
	}

	public void Close()
	{
		this.QueueFree();
	}

	private void ModifyMessage(string message)
	{
		this._message.Text = message ;
	}
}
