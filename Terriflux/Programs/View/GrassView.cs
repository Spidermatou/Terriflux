using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.View
{
    public partial class GrassView : CellView
    {
        public static readonly string TEXTURE_PATH = OurPaths.TEXTURES + "new_grass.png";

        /// <summary>
        /// Create a view for a grass cell.
        /// Careful: Simple class construction not allowed. Please use the associated Design() function!
        /// </summary>
        protected GrassView() : base() { }

        /// <summary>
        /// Design a GrassView. 
        /// Remember to add it to your scene to display it!
        /// </summary>
        /// <returns></returns>
        public static new GrassView Design()
        {
            return (GrassView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "GrassView" + OurPaths.NODEXT)
                .Instantiate();
        }

        public override void _Ready()
        {
            base._Ready();

            // Update texture and showed name
            ChangeSkin(GD.Load<Texture2D>(TEXTURE_PATH));
            ChangeName("Grass");
        }
    }
}
