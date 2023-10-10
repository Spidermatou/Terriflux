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
            // Reset
            RemoveAllChildren();

            // Construct the graphical grid
            for (int x = 0; x < grid.GetSize(); x++)
            {
                for (int y = 0; y < grid.GetSize(); y++)
                {
                    CellModel cell = grid.GetAt(x, y);
                    IPlaceable placeable = grid.GetPlaceableAt(x, y);

                    // Is there a building here?
                    if (cell != null
                        && cell.GetCellKind() == CellKind.BUILDING 
                        && placeable != null 
                        && placeable is BuildingModel buildingModel)
                    {
                        this.AddChild(BuildingView.Design(this, buildingModel));
                    }
                    else
                    {
                        // Fill with simple grass
                        GrassView grass = (GrassView)GrassView.Design();
                        GrassModel model = new();   // just for dimensions
                        
                        // Instantiate into scene
                        grass.Position = new Vector2(x * model.GetExactDimensions(), y * model.GetExactDimensions());
                        this.AddChild(grass);
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