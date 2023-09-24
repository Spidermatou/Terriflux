using Godot;
using System;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.GameContext;

public partial class CellView : Node2D, ICellObserver, IVerbosable
{
	// children
    private Label _nicknameLabel;
    private Sprite2D _skin;

    private CellView() { }

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

    /// <summary>
    /// Change printed sprite.
    /// </summary>
    /// <param name="skinName">Name of png/svg file to load, with extension precised</param>
    public void ChangeSkin(string skinName)
    {
        skinName = skinName.ToLower();
        this._skin.Texture = GD.Load<Texture2D>(Paths.IMAGES + skinName);
    }

    /// <summary>
    /// Change printed sprite.
    /// </summary>
    /// <param name="skinName">Name of png/svg file to load</param>
    /// <param name="extension">His extension (.png, .svg, .jpeg, etc.) </param>
    public void ChangeSkin(string skinName, string extension)
    {
        skinName = skinName.ToLower();
        this._skin.Texture = GD.Load<Texture2D>(Paths.IMAGES + skinName + extension);
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
}
