using Godot;
using System;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.View;

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
            Test_BuildGeneration();
        }

        public override void _Process(double delta)
        {
        }

        private void Test_Grid()
        {
            // test grid
            Grid grid_test = GridFactory.CreateFullGrassLand(10);
            TerritoryView territory_view = TerritoryView.Create();
            this.AddChild(territory_view);
            grid_test.SetObserver(territory_view);
            territory_view.UpdateMap(grid_test);
        }

        private void Test_EnumTranslation() 
        {
            // enum test
            GD.Print("---");
            GD.Print("Enum test:");
            string what = "wAteR";
            what = what.Capitalize();
            GD.Print(GlobalTools.TranslateToFlowKind(what));

        }

        private void Test_BuildGeneration()
        {
            TerritoryView territory_view = TerritoryView.Create();
            this.AddChild(territory_view);

            GD.Print("Build generation test:");
            
            BuildingModel model = BuildingFactory.CreateBuilding("fIElD");
            BuildingView view = BuildingFactory.DesignBuilding(territory_view, model);
            territory_view.AddChild(view);

        }
    }
}
