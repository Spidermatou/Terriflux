using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model
{
    public static partial class CellsFactory
    {
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

        // TODO - public static BuildingModel CreateBuilding(string name)
        /* reads a file containing all the information related to each building available in the game, 
         * and retrieves those associated with the name specified in the parameter. */

        public static BuildingView DesignBuilding(Node parent, BuildingModel model) // TODO
        {
            string texturePath = Paths.TEXTURES + model.GetName() + ".png";
            if (!File.Exists(texturePath))
            {
                throw new FileNotFoundException(texturePath + " file doesn't found.");
            }
            BuildingView bv = BuildingView.Create(model.GetName());
            parent.AddChild(bv); // instantiate this and his children
            bv.ChangeSkin(texturePath);

            // observer and update
            model.AddObserver(bv);
            model.NotifyAlls();

            return bv;
        }
    }
}