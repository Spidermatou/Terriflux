using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs.Factories;
using Terriflux.Programs.Model.Cell;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

namespace Terriflux.Programs.GameContext
{
    public partial class GridView : Node2D, IGridObserver
    {
        /// <summary>
        /// Create a view for any grid.
        /// Careful: Simple class construction not allowed. Please use the associated Design() function!
        /// </summary>
        private GridView() { }

        public static GridView Design()
        {
            return (GridView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "GridView" + OurPaths.GDEXT)
                .Instantiate();
        }

        /// <summary>
        /// Update the specified grid on the screen.
        /// </summary>
        /// <param name="grid"></param>
        /// <exception cref="NotImplementedException"></exception>
		public void UpdateMap(GridModel grid)
        {
            /* This version of the grid view update destroys the old grid (view), 
             * then scans each cell of the model to check whether an element is placed there. 
             * Optimization is surely possible. */

            bool emptyCell = false;
            Dictionary<Vector2I, CellModel> haveToPlace = grid.GetAllPlacements();

            // reset
            RemoveAllChildren();

            // construct the graphical grid
            for (int x = 0; x < grid.GetSize(); x++)
            {
                for (int y = 0; y < grid.GetSize(); y++)
                {
                    Vector2I actualCoordinates = new(x, y);

                    // is there a placeable object here?
                    if (haveToPlace.ContainsKey(actualCoordinates))       // yes
                    {
                        // is it a building?
                        if (haveToPlace[actualCoordinates] is BuildingModel buildingModel)    // yes
                        {
                            BuildingView buildingView = BuildingFactory.CreateView(buildingModel);
                            buildingView.Position = new Vector2((float)(x * CellView.GetGlobalSize()), (float)(y * CellView.GetGlobalSize()));
                            AddChild(buildingView);
                        }
                        else    // yes, but it's not treatable 
                        {
                            emptyCell = true;
                        }
                    }
                    else        // no, it's a free case (i.e. grass cell or empty cell)
                    {
                        emptyCell = true;
                    }


                    // Have to design grass to fill an invalid case?
                    if (emptyCell)
                    {
                        // Fill with simple grass
                        GrassView grass = GrassView.Design();

                        // Instantiate into scene
                        grass.Position = new Vector2((float)(x * CellView.GetGlobalSize()), (float)(y * CellView.GetGlobalSize()));
                        AddChild(grass);

                        // Next case probably valid
                        emptyCell = false;
                    }
                }
            }
        }

        private void RemoveAllChildren()
        {
            foreach (Node child in GetChildren())
            {
                RemoveChild(child);
            }
        }

    }
}