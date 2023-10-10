using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

namespace Terriflux.Programs.GameContext
{
    public partial class GridView : Node2D, IGridObserver
    {
        private Camera2D _camera;

        /// <summary>
        /// Simple class construction not allowed. Please use the associated Design() function.
        /// </summary>
        private GridView() { }

        public static GridView Design()
        {
            return (GridView)GD.Load<PackedScene>(Paths.VIEW_NODES + "GridView" + Paths.GDEXT)
                .Instantiate();
        }

        public override void _Ready()
        {
            // nodes
            _camera = GetNode<Camera2D>("Camera");
        }


        /// <summary>
        /// Update the specified grid on the screen.
        /// </summary>
        /// <param name="grid"></param>
        /// <exception cref="NotImplementedException"></exception>
		public void UpdateMap(GridModel grid)       // TODO - Urgent - have some troubles here
        {
            bool invalidCase = false;

            // Reset
            RemoveAllChildren();

            // Construct the graphical grid
            for (int x = 0; x < grid.GetSize(); x++)
            {
                for (int y = 0; y < grid.GetSize(); y++)
                {
                    Dictionary<Tuple<int, int>, IPlaceable> placeables = grid.GetPlaceablesInfos();
                    Tuple<int, int> actualCoordinates = new(x, y);

                    // Is there a building here?
                    if (placeables.ContainsKey(actualCoordinates))   // is a building on this coordinates?
                    {
                        // Is a building?
                        if (placeables[actualCoordinates] is BuildingModel buildingModel)
                        {
                            // Load view and instantiate her on the scene
                            BuildingView building = BuildingViewsFactory.DesignFromModel(buildingModel, this);
                            building.Position = new Vector2((float)(x * CellModel.GetDefaultDimension()),
                                (float)(y * CellModel.GetDefaultDimension()));
                        }
                        else
                        {
                            invalidCase = true;
                        }
                    }
                    else
                    {
                        invalidCase = true;
                    }
                    

                    // Have to design grass to fill an invalid case?
                    if (invalidCase)
                    {
                        // Fill with simple grass
                        GrassView grass = (GrassView)GrassView.Design();
                        GrassModel model = new();   // just for dimensions
                        
                        // Instantiate into scene
                        grass.Position = new Vector2(x * model.GetExactDimensions(), y * model.GetExactDimensions());
                        this.AddChild(grass);

                        // Next case probably valid
                        invalidCase = false;
                    }
                }
            }
        }

        private void RemoveAllChildren()
        {
            foreach (Node child in this.GetChildren())
            {
                this.RemoveChild(child);
            }
        }

    }
}