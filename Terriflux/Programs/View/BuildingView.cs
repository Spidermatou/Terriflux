using Godot;
using System;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Placeables;

namespace Terriflux.Programs.View
{
    /// <summary>
    /// Creates a building visible for the player (IMMUTABLE!)
    /// </summary>
    public partial class BuildingView : CellView
    {
        private string name;
        private Texture2D texture;
        private Sprite2D _grassFill;

        /// <summary>
        /// Create a view for any building.
        /// Careful: Simple class construction not allowed. Please use the associated Design() function!
        /// </summary>
        private BuildingView() : base() { }

        /// <summary>
        /// Design a Building Part View. 
        /// Remember to add it to your scene to display it
        /// </summary>
        /// <param name="model">The model on which the view is based.</param>
        /// <returns></returns>
        public static BuildingView Design(BuildingModel model, Texture2D texture)
        {
            // security
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            BuildingView view = (BuildingView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "BuildingView" + OurPaths.NODEXT)
                    .Instantiate();

            view.name = model.GetName();
            view.texture = texture;
            return view;
        }

        public override void _Ready()
        {
            base._Ready();

            // get nodes
            _grassFill = GetNode<Sprite2D>("GrassFill");
            _grassFill.Texture = GD.Load<Texture2D>(GrassView.TEXTURE_PATH);
            _grassFill.Scale = new Vector2((float)CellView.GetGlobalSize(), (float)CellView.GetGlobalSize()) / _grassFill.Texture.GetSize();

            // update
            ChangeName(name);
            ChangeSkin(texture);
        }
    }
}