using Godot;
using Terriflux.Programs.Controller;
using Terriflux.Programs.Factories;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Gauges;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Round;

namespace Terriflux.Programs.View
{
    public partial class MainScene : Node2D
    {
        private const int GAME_GRID_SIZE = 8;

        private readonly RoundModel roundModel = new();
        private readonly GridModel gridModel = GridFactory.CreateWasteland(GAME_GRID_SIZE);


        // nodes
        private RoundCounter _roundView;
        private GridView _gridView;
        private Impacts _impactsView;

        private MainScene() : base() { }

        public override void _Ready()
        {
            base._Ready();

            /* ************
             * get children
             * */
            _roundView = GetNode<RoundCounter>("RoundCounter");
            _impactsView = GetNode<Impacts>("Impacts");
            _gridView = GetNode<GridView>("Grid");

            /* ************
             * config grid
             * */
            const float DIMENSION = (float) 0.6;
            _gridView.Scale = new Vector2(DIMENSION, DIMENSION);       // reduces size scale to be clearly visible
            gridModel.AddObserver(_gridView);        // add view as observer
            gridModel.ForceUpdate();


            /* ************
             * config the static grid controller
             * */
            GridController.SetGrid(gridModel);   // have to manage this new grid model
            GridController.SetRoundManager(roundModel);   // have to manage this new round model
            GridController.SetControledImpacts(_impactsView);   // have to manage this new impacts model

            /* ************
             * config the rounds
             * */
            RoundController.SetModel(roundModel);
            RoundController.SetView(_roundView);
        }
    }
}
