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
            GridModel grid_test = GridFactory.CreateFullGrassLand(10);
            GridView territory_view = GridView.Design();
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
            GridView territory_view = GridView.Design();
            this.AddChild(territory_view);

            GD.Print("Build generation test:");
            
            BuildingModel model = BuildingFactory.CreateFromName("fIElD");
            BuildingView view = BuildingFactory.Design(territory_view, model);
            territory_view.AddChild(view);

        }

        private void Test_PlaceBuilding()
        {
            // Game basis creation
            GridModel grid = new(5);
            GridView view = GridView.Design();
            grid.SetObserver(view);

            // Building creation
            BuildingModel bmodel = BuildingFactory.CreateFromName("field");
            grid.PlaceAt(bmodel, new Vector2I(1,1), true);

        }
    }
}
