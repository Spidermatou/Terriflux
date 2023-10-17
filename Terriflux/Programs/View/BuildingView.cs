using Godot;
using System;
using System.Dynamic;
using System.IO;
using System.Xml.Linq;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
    /// <summary>
    /// Creates a building visible for the player (IMMUTABLE!)
    /// </summary>
    public partial class BuildingView : Node2D      
    {
        private BuildingModel model;
        private Orientation2D orientation;
        private Texture2D[] allPartsTextures;
        private BuildingPartView[] parts;     // create after ready


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
        public static BuildingView Design(BuildingModel model, Orientation2D orientation, Texture2D[] partsTextures)    
        {
            // security
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else if (model.GetComposition().Length == 0)
            {
                throw new Exception(nameof(model) + " have no part into him.");
            }
            else if (partsTextures == null)
            {
                throw new ArgumentNullException(nameof(partsTextures));
            }
            else if (partsTextures.Length != model.GetPartsNumber())
            {
                throw new Exception("Not enough / too much textures to design building.");
            }

            BuildingView view = (BuildingView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "BuildingView" + OurPaths.GDEXT)
                    .Instantiate();
            view.model = model;
            view.orientation = orientation;
            view.allPartsTextures = partsTextures;
            return view;
        }

        public override void _Ready()       
        {
            base._Ready();

            string name = model.GetName();
            this.parts = new BuildingPartView[model.GetPartsNumber()];

            for (int i = 0; i < this.model.GetPartsNumber(); i++)
            {
                // Crete a new part view, each with a piece of the global texture, and the same name as the model
                BuildingPartView part = BuildingPartView.Design(name, this.allPartsTextures[i]);
                this.parts[i] = part;
            }

            /* CREATE HIM ON THE SCREEN */
            Vector2 origin = this.Position;
            // GD.Print($"BEFORE #{this.GetChildren().Count}");     // test


            // first part needs no offset nor calculation.
            this.parts[0].Position = new Vector2(origin.X, origin.Y);        // position into his parent (= into the building view itself)
            this.AddChild(this.parts[0]);

            // add other parts with a correction offset
            for (int i = 1; i < this.parts.Length; i++)
            {
                if (this.orientation == Orientation2D.HORIZONTAL)
                {
                    float offsetPositionI = (float)(origin.X + (CellModel.GetGlobalSize() / this.parts.Length * i));    // size_of_a_cell / nb_sliced_part * part_position + correction_offset
                    this.parts[i].Position = new Vector2(offsetPositionI - (1 * i), origin.Y);
                }
                else
                {
                    float offsetPositionI = (float)(origin.Y + (CellModel.GetGlobalSize() / this.parts.Length * i));    
                    this.parts[i].Position = new Vector2(origin.X, offsetPositionI - (1 * i));
                }
                this.AddChild(this.parts[i]);
            }

            // GD.Print($"AFTER #{this.GetChildren().Count}");      // test

        }
    }
}