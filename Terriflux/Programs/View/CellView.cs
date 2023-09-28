using Godot;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.GameContext;

public partial class CellView : Node2D, ICellObserver, IVerbosable
{
	// children
    private Label _nicknameLabel;
    private Sprite2D _skin;

    protected CellView() { }

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

    public void UpdatePlacement(Vector2I coordinates)
    {
        this.Position = coordinates;
    }

    public void UpdateCellName(string cname)
    {
        this._nicknameLabel.Text = cname;
    }

    public void ChangeSkin(string path)
    {
        if (this._skin == null)
        {
            throw new NullReferenceException(this + "'s skin child not loaded correctly!");
        }

        if (File.Exists(path))
        {
            this._skin.Texture = GD.Load<Texture2D>(path);
        }
        else
        {
            GD.Print(path); // test
            throw new ArgumentException("Invalid texture path.");
        }
    }

    public static CellView Create()
    {
        return (CellView)GD.Load<PackedScene>(Paths.VIEW_NODES + "CellView" + Paths.GDEXT)
            .Instantiate();
    }

    public string Verbose()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Cell " + this);
        if (_skin == null)
        {
            sb.Append("_skin null");
        }
        else
        {
            sb.Append("Skin = " + _skin.Texture.ResourceName);
        }
        if (_nicknameLabel == null)
        {
            sb.Append("_nicknameLabel null");
        }
        return sb.ToString();
    }

    public void UpdateCellKind(CellKind kind)
    {
        // Does nothing
    }
}
