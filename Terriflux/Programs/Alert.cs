using Godot;
using System;
using System.Text;

namespace Terriflux.Programs;

/// <summary>
/// Singleton.
/// Must be added as a scene child only once (min and max) to function.
/// </summary>
public partial class Alert : RawNode
{
    // himself
    private static readonly Alert singleton = (Alert) RawNode.Instantiate("Alert");

	// children
	Label _message;

    private Alert() : base() { }

    public override void _Ready()
	{
		base._Ready();
		this._message = GetNode<Label>("Message");
		this.GlobalPosition = new Vector2(-10, -10); 
		this.ZIndex = 600;
		Close();
    }

    public static Alert GetInstance()
	{
		return singleton;
	}

    /// <summary>
    /// Displays the dialog box with the specified message.
    /// </summary>
    /// <param name="message"></param>
    public static void Say(string message)
	{
        // show the message
        ModifyMessage(message);
        singleton.Show();
	}

	public void Close()
	{
        singleton.Hide();
	}

	private static void ModifyMessage(string message)
	{
        singleton._message.Text = message ;
	}
}
