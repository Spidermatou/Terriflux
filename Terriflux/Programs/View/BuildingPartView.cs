using Godot;
using System;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.View;

namespace Terriflux.Programs.View
{
    public partial class BuildingPartView : CellView        // Reworked
    {
        public static readonly string buildingPartTexturePath = OurPaths.TEXTURES + "building_default" + OurPaths.PNGEXT;

        private string name;
        private Texture2D texture;

        /// <summary>
        /// Create a view for any building part
        /// Careful: Simple class construction not allowed. Please use the associated Design() function!
        /// </summary>
        protected BuildingPartView() : base() { }

        /// <summary>
        /// Design a Building Part View. 
        /// Remember to add it to your scene to display it!
        /// </summary>
        /// <returns></returns>
        public static new BuildingPartView Design()
        {
            return (BuildingPartView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "BuildingPartView" + OurPaths.GDEXT)
                .Instantiate();
        }

        /// <summary>
        /// Design a Building Part View. 
        /// Remember to add it to your scene to display it!
        /// </summary>
        /// <param name="texture">What image will be display on this building part.</param>
        /// <returns></returns>
        public static BuildingPartView Design(string buildingName, Texture2D texture)       
        {
            BuildingPartView buildPartView = (BuildingPartView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "BuildingPartView" + OurPaths.GDEXT)
                .Instantiate();
            buildPartView.texture = texture;
            buildPartView.name = buildingName;
            return buildPartView;
        }

        public override void _Ready()
        {
            base._Ready();

            // Update texture
            if (name != null) ChangeName(name);
            else ChangeName("BuildingPart");

            // Showed name
            if (this.texture != null) ChangeSkin(texture);
            else ChangeSkin(GD.Load<Texture2D>(buildingPartTexturePath));
        }
    }
}
