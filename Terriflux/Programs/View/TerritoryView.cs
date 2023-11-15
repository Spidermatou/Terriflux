using Godot;
using System;
using System.Collections.Generic;

namespace Terriflux.Programs.GameContext
{
	public partial class TerritoryView : Node2D
	{
		private List<CellModel> grid = new List<CellModel>();

		public override void _Ready()
		{
			// test
			GenerateMap(10, 10);
		}


		// all in model, no value stored here, just map generation
		private void GenerateMap(int lines, int columns)
		{
			for (int x = 0; x < lines; x++)
			{
				CellModel model = new(); // for size and pos
				for (int y = 0; y < columns; y++)
				{
					CellView cv = new CellView();
                    CellModel cm = new CellModel(cv, 
						model.GetCellSize() * x,
						model.GetCellSize() * y
						);

					// instatiate into this
					cv.Initialize(this, (int) cm.GetPlacement().X, (int) cm.GetPlacement().Y);

					// add the cell to the grid-data
					grid.Add(cm);
				}
			}
		}
	}
}
