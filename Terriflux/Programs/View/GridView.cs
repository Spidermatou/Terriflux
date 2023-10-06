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

        private GridView() { }

        public static GridView Design()
        {
            return (GridView)GD.Load<PackedScene>(Paths.VIEW_NODES + "TerritoryView" + Paths.GDEXT)
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
                    CellView cv;
                    CellKind actKind = grid.GetAt(x, y).GetCellKind();
                    if (actKind == CellKind.BUILDING)
                    {
                        // TODO DesignBuilding
                        throw new NotImplementedException();
                    }
                    else
                    {
                        // Instantiate view
                        cv = GrassView.Design(this, grid.GetAt(x, y));
                    }

                    // Link view and model
                    grid.GetAt(x, y).AddObserver(cv);
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