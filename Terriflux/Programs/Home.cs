using Godot;
using System;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;

namespace Terriflux.Programs
{
    /// <summary>
    /// Start the game
    /// </summary>
    public partial class Home : Node2D
    {
        private Home() { }

        public override void _Ready()
        {
            // test grid
            Grid grid_test = GridFactory.CreateFullGrassLand(10);
            TerritoryView territory_view = TerritoryView.Create();
            this.AddChild(territory_view);
            grid_test.SetObserver(territory_view);
            territory_view.UpdateMap(grid_test);

            // test building
            // todo


        }

        public override void _Process(double delta)
        {
        }
    }
}
