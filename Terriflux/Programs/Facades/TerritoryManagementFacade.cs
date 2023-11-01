using Godot;
using Terriflux.Programs.Controller;
using Terriflux.Programs.Factories;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Round;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Facades
{
    public class TerritoryManagementFacade
    {
        private readonly Node root;

        public TerritoryManagementFacade(Node root)
        {
            this.root = root;
        }

        public void CreateAndConfigGrid(Vector2 gridPosition, int gridSize,
            out GridModel gridModel, out GridView gridView)
        {
            // grid
            gridModel = GridFactory.CreateWasteland(gridSize);    // create model
            gridView = GridFactory.CreateView(gridModel);      // create view
            gridView.Position = gridPosition;       // place it
            gridView.Scale = new Vector2((float)0.5, (float)0.5);       // reduces size scale to be clearly visible
            gridModel.AddObserver(gridView);        // add view as observer
            this.root.AddChild(gridView);       // add view to scene

            // config the static grid controller
            GridController.SetGridControl(gridModel);   // have to manage this new grid model
        }

        public void ConfigRounds(RoundModel roundModel, RoundCounter roundView)
        {
            RoundController.SetModel(roundModel);
            RoundController.SetView(roundView);
        }
    }
}
