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
		public void UpdateMap(GridModel grid)
        {
            // Reset
            RemoveAllChildren();

            // Construct the graphical grid
            for (int x = 0; x < grid.GetSize(); x++)
            {
                for (int y = 0; y < grid.GetSize(); y++)
                {
                    // Is there a building here?
                    if (grid.GetAt(x, y).GetCellKind() == CellKind.BUILDING)
                    {
                        //BuildingView buildingView = BuildingView.Design(this,)
                        // TODO DesignBuilding
                        // TODO - NotImplemented
                    }
                    else
                    {
                        // Fill with simple grass
                        grid.GetAt(x, y).AddObserver( GrassView.Design(this, grid.GetAt(x, y)) );
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