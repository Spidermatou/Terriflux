using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Terriflux.Programs.GameContext
{
	public partial class TerritoryView : Node2D, IGridObserver
	{
        private Camera2D _camera;

		public override void _Ready()
		{
            // nodes
            _camera = GetNode<Camera2D>("Camera");
        }

		public void UpdateMap(Grid grid)
		{
            // Construct the graphical grid
            CellsFactory cf = new();
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
                        cv = cf.DesignGrass(this, grid.GetAt(x, y));
                    }

                    // Link view and model
                    grid.GetAt(x, y).SetObserver(cv);
                }
            }
		}
    }
}