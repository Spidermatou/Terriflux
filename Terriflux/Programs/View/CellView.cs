using Godot;
using System;
using Terriflux.Programs.GameContext;

public partial class CellView : Node2D, ICellObserver
{
	// children
    private Label _nicknameLabel;
    private Sprite2D _skin;

    public override void _Ready()
    {
        _nicknameLabel = GetNode<Label>("NicknameLabel");
        _skin = GetNode<Sprite2D>("Skin");

        // Hide useless
        _nicknameLabel.Hide();

        // default
        _nicknameLabel.Text = "Cell";
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

    public void UpdatePlacement(Vector2 coordinates)
    {
        this.Position = coordinates;
    }

    public void UpdateCellName(StringName cname)
    {
        this._nicknameLabel.Text = cname;
    }

    public void LoadSkin()
    {
        // TODO
    }

    public void Initialize(Node root, int x, int y)
    {
        Node2D n = (Node2D) GD.Load<PackedScene>(Paths.VIEW_NODES + "CellView" + Paths.GDEXT)
            .Instantiate();
        n.Position = new Vector2(x, y);
        root.AddChild(n); 


    }

}
