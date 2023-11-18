using Godot;
using System;

public partial class ExitButton : Button
{
    private readonly Color NORMAL_COLOR = new("933364");
	private readonly Color HOVER_COLOR = new("370c83");

    // children
    private Polygon2D _background;

    public override void _Ready()
    {
        base._Ready();

        _background = this.GetNode<Polygon2D>("ExitBackground");
        _background.Color = NORMAL_COLOR;
    }

    // Feedback
    /// <summary>
    /// When is clicked
    /// </summary>
    private void OnPressed()
	{
		GetTree().Quit();
	}

    /// <summary>
    /// When mouse is hover
    /// </summary>
    private void OnMouseHover() {
        _background.Color = HOVER_COLOR;
    }

    /// <summary>
    /// When mouse is not hover
    /// </summary>
    private void OnMouseExit()
    {
        _background.Color = NORMAL_COLOR;
    }

}
