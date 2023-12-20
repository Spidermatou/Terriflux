using Godot;
using System;
using Terriflux.Programs;

public partial class Round : RawNode, IRound
{
    private Button number;

	public Round() { }

    public int GetNumber()
    {
        return int.Parse(number.Text);
    }

    public void Next()
    {
        number.Text = (int.Parse(number.Text) + 1).ToString();
    }

    public override void _Ready()
	{
		base._Ready();

        number = GetNode<Button>("Number");

        number.Text = "1";
	}

	private void OnPressed()
    {
        Next();
    }
}
