using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.View
{
    public partial class GrassView : CellView
    {
        private static readonly string texturePath = Paths.TEXTURES + "grass.png";

        private GrassView() { }

        /// <summary>
        /// Instantiate a CellView with correct kind, change his skin to
        /// grass and add to specified node's (root) tree.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="model"></param>
        /// <returns>The already-added CellView.</returns>
        public static new CellView Design()
        {
            return (CellView)GD.Load<PackedScene>(Paths.VIEW_NODES + "GrassView" + Paths.GDEXT)
                .Instantiate();
        }

        public override void _Ready()
        {
            base._Ready();

            // Update texture and showed name
            ChangeSkin(texturePath);
            UpdateCellName("Grass");
        }
    }
}
