using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;

public static partial class CellsFactory 
{
    /// <summary>
    /// Create a very basic cell, with all default values
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static CellModel CreatePrimaryCell(int x, int y)
    {
        return new(x, y);
    }

    /// <summary>
    /// Create a very basic cell, with default texture
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="model"></param>
    /// <returns>The already-added CellView.</returns>
    public static CellView DesignPrimaryCell(Node parent, CellModel model)
    {
        CellView cv = CellView.Create();
        cv.Position = model.GetPlacement();
        parent.AddChild(cv); // instantiate this and his children
        return cv;
    }


    public static CellModel CreateGrass(int x, int y)
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
    public static CellView DesignGrass(Node parent, CellModel model)
    {
        CellView cv = CellView.Create();
        cv.Position = model.GetPlacement(); 
        parent.AddChild(cv); // instantiate this and his children
        cv.ChangeSkin(Paths.TEXTURES + "grass.png");
        cv.UpdateCellName(model.GetCellName());
        return cv;
    }

    public static BuildingModel CreateBuilding(string name)
    {
        // TODO
        return null;
    }

    public static BuildingModel dev_CreateB()
    {
        //return new BuildingModel(name.Capitalize(), )
        return null;

    }

    public static BuildingView DesignBuilding(Node parent, BuildingModel model) // TODO
    {
        string texturePath = Paths.TEXTURES + model.GetCellName() + ".png";
        if (!File.Exists(texturePath))
        {
            throw new FileNotFoundException(texturePath+" file doesn't found.");
        }
        BuildingView bv = BuildingView.Create(model.GetCellName());
        parent.AddChild(bv); // instantiate this and his children
        bv.ChangeSkin(texturePath);

        // observer and update
        model.AddObserver(bv);
        model.NotifyAllBuildingInfos();

        return bv;
    }


}
