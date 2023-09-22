using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Terriflux.Programs.GameContext
{
	public partial class TerritoryView : Node2D
	{
		public override void _Ready()
		{
            // test
            GridGenerator gd = new();
            Grid test = gd.GenerateGrid(10);
            UpdateMap(test);
        }

		private void UpdateMap(Grid grid)
		{
			for (int x = 0; x < grid.GetSize(); x++)
			{
                for (int y = 0; y < grid.GetSize(); y++)
				{
                    // Instantiate view
                    CellView cv = CellView.Create();
                    cv.Position = grid.GetAt(x, y).GetPlacement();
                    this.AddChild(cv);

                    // Link view and model
                    grid.GetAt(x, y).SetObserver(cv);
                }
            }
		}
    }
}