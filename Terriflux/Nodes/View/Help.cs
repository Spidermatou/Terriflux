using Godot;
using System;
using System.Runtime.CompilerServices;
using Terriflux.Programs.GameContext;

public partial class Help : TextureButton
{
	private IVerbosable verbosableObject;

	private Help() { } // for godot
	public static Help Design() { return (Help) GD.Load<PackedScene>(OurPaths.VIEW_NODES + "Help.tscn").Instantiate(); }

    public override void _Ready()
    {
        base._Ready();

		this.TextureNormal = GD.Load<Texture2D>(OurPaths.IMAGES + "Help.png");
		this.TextureHover = GD.Load<Texture2D>(OurPaths.IMAGES + "HelpHover.png");
    }

    public String Get()
	{
		if (this.verbosableObject == null) throw new NullReferenceException();
		return this.verbosableObject.Verbose();
	}

	public void Set(IVerbosable verbosableObject) { this.verbosableObject = verbosableObject; }

	public void Say()
	{
		GD.Print(this.Get());
	}

	// SIGNAL
	private void OnHelpButtonPressed() { this.Say(); }

}
