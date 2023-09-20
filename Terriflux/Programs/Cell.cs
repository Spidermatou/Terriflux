using Godot;
using System;

public partial class Cell : Node2D
{
	// values
	protected StringName nickname = "Cell";
    protected int CELL_SIZE = 128; // px


    // children
    private Label _nicknameLabel;

    public override void _Ready()
    {
		_nicknameLabel = GetNode<Label>("NicknameLabel");
		_nicknameLabel.Text = nickname;
		_nicknameLabel.Hide();
    }

    public int GetCellSize()
    {
        return this.CELL_SIZE;
    }

    public void SetCellSize(int newSellSize)
    {
        this.CELL_SIZE = newSellSize;
    }

    private void OnMouseOver()
	{
		_nicknameLabel.Show();
	}

    private void OnMouseExit()
    {
        _nicknameLabel.Hide();
    }
}
