using Godot;
using System;
using Terriflux.Programs.GameContext;

public partial class CellsFactory 
{
    public CellModel CreateGrass(int x, int y)
    {
        CellModel cm = new("Grass", CellKind.WASTELAND, x, y);
        return cm;
    }

    /// <summary>
    /// Instantiate a CellView with correct kind, change his skin to
    /// grass and add to specified node's (root) tree.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="model"></param>
    /// <returns>The already-added CellView.</returns>
    public CellView DesignGrass(Node root, CellModel model)
    {
        CellView cv = CellView.Create();
        cv.Position = model.GetPlacement(); 
        root.AddChild(cv); // instantiate this and his children
        cv.ChangeSkin(Paths.TEXTURES + "grass.png");
        return cv;
    }

}
