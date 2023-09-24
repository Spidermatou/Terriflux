using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Terriflux.Programs.GameContext
{
	public partial class TerritoryView : Node2D, IGridObserver
	{
		public override void _Ready()
		{
            // test
            GridFactory gd = new();
            Grid test = gd.CreateNoMansLand(10);
            UpdateMap(test);
        }

		public void UpdateMap(Grid grid)
		{
			for (int x = 0; x < grid.GetSize(); x++)
			{
                for (int y = 0; y < grid.GetSize(); y++)
				{
                    // Instantiate view
                    CellView cv = CellView.Create();
                    cv.Position = grid.GetAt(x, y).GetPlacement();
                    this.AddChild(cv);

                    // Change skin
                    cv.ChangeSkin(grid.GetAt(x, y).GetCellName(),
                        grid.GetAt(x, y).GetSkinExtension());

                    // Link view and model
                    grid.GetAt(x, y).SetObserver(cv);
                }
            }
		}
    }
}