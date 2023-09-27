using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Terriflux.Programs.GameContext
{
    public partial class TerritoryView : Node2D, IGridObserver
    {
        private Camera2D _camera;

        private TerritoryView() { }

        public static TerritoryView Create()
        {
            return (TerritoryView)GD.Load<PackedScene>(Paths.VIEW_NODES + "TerritoryView" + Paths.GDEXT)
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
		public void UpdateMap(Grid grid)
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
                        cv = null;
                        throw new NotImplementedException();
                    }
                    else
                    {
                        // Instantiate view
                        cv = CellsFactory.DesignGrass(this, grid.GetAt(x, y));
                    }

                    // Link view and model
                    grid.GetAt(x, y).SetObserver(cv);
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