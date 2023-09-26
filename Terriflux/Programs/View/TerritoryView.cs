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


            // test
            GridFactory gd = new();
            Grid test = gd.CreateNoMansLand(10);
            UpdateMap(test);
        }

		public void UpdateMap(Grid grid)
		{
            // Zoom in or out
            if (grid.GetSize() >= 10)
            {
                _camera.Zoom = new Vector2(1, 1); // TODO
            }
            else
            {
                _camera.Zoom = new Vector2(1, 1);
            }

            // Construct the graphical grid
            CellsFactory cf = new();
			for (int x = 0; x < grid.GetSize(); x++)
			{
                for (int y = 0; y < grid.GetSize(); y++)
				{
                    // Instantiate view
                    CellView cv = cf.DesignGrass(this, grid.GetAt(x, y));

                    // Link view and model
                    grid.GetAt(x, y).SetObserver(cv);
                }
            }
		}
    }
}