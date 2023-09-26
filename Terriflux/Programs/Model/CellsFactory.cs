using Godot;
using Terriflux.Programs.GameContext;

public partial class CellsFactory 
{
    /// <summary>
    /// Create a very basic cell, with all default values
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public CellModel CreatePrimaryCell(int x, int y)
    {
        return new(x, y);
    }

    /// <summary>
    /// Create a very basic cell, with default texture
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="model"></param>
    /// <returns>The already-added CellView.</returns>
    public CellView DesignPrimaryCell(Node parent, CellModel model)
    {
        CellView cv = CellView.Create();
        cv.Position = model.GetPlacement();
        parent.AddChild(cv); // instantiate this and his children
        return cv;
    }


    public CellModel CreateGrass(int x, int y)
    {
        CellModel cm = new("Grass", CellKind.WASTELAND, x, y);
        return cm;
    }

    /// <summary>
    /// Instantiate a CellView with correct kind, change his skin to
    /// grass and add to specified node's (root) tree.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="model"></param>
    /// <returns>The already-added CellView.</returns>
    public CellView DesignGrass(Node parent, CellModel model)
    {
        CellView cv = CellView.Create();
        cv.Position = model.GetPlacement(); 
        parent.AddChild(cv); // instantiate this and his children
        cv.ChangeSkin(Paths.TEXTURES + "grass.png");
        cv.UpdateCellName(model.GetCellName());
        return cv;
    }

}
