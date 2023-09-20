using Godot;
using System;

public partial class Cell : Node2D
{
	// values
	private StringName cname = "Cell";
    private int size = 128; // px
    private CellKind kind = CellKind.PRIMARY;

    // children
    private Label _nicknameLabel;

    public override void _Ready()
    {
		_nicknameLabel = GetNode<Label>("NicknameLabel");
		_nicknameLabel.Text = cname;
		_nicknameLabel.Hide();
    }

    // GET-SET
    public int GetCellSize()
    {
        return this.size;
    }

    public void SetCellSize(int newSellSize)
    {
        this.size = newSellSize;
    }

    public void SetCellName(StringName newName)
    {
        this.cname = newName;
    }

    public StringName GetCellName()
    {
        return this.cname;
    }

    public void SetCellKind(CellKind newKind)
    {
        this.kind = newKind;
    }

    public CellKind GetCellKind()
    {
        return this.kind;
    }

    // Events
    private void OnMouseOver()
	{
		_nicknameLabel.Show();
	}

    private void OnMouseExit()
    {
        _nicknameLabel.Hide();
    }
}
