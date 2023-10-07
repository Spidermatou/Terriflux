using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.View
{
    public partial class GrassView
    {
        private GrassView() { }

        /// <summary>
        /// Instantiate a CellView with correct kind, change his skin to
        /// grass and add to specified node's (root) tree.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="model"></param>
        /// <returns>The already-added CellView.</returns>
        public static CellView Design(Node parent, CellModel model)
        {
            CellView cv = CellView.Design();
            cv.Position = model.GetPlacement();
            parent.AddChild(cv); // instantiate this and his children
            cv.ChangeSkin(Paths.TEXTURES + "grass.png");
            cv.UpdateCellName(model.GetCellName());
            return cv;
        }
    }
}
