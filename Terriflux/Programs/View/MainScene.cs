using Godot;
using Terriflux.Programs.Controller;
using Terriflux.Programs.Factories;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Round;

namespace Terriflux.Programs.View
{
    public partial class MainScene : Node2D
    {
        private const int gameGridSize = 10;

        private readonly RoundModel roundModel = new();
        private readonly GridModel gridModel = GridFactory.CreateWasteland(gameGridSize);


        // nodes
        private RoundCounter _roundView;
        private GridView _gridView;

        // markers
        private Marker2D _markGrid;

        private MainScene() : base() { }

        public override void _Ready()
        {
            base._Ready();

            /* ************
             * get children
             * */
            _markGrid = GetNode<Marker2D>("GridMark");
            _roundView = GetNode<RoundCounter>("RoundCounter");


            /* ************
             * config grid
             * */
            _gridView = GridFactory.CreateView(gridModel);      // create view
            _gridView.Position = _markGrid.Position;       // place it
            _gridView.Scale = new Vector2((float)0.5, (float)0.5);       // reduces size scale to be clearly visible
            gridModel.AddObserver(_gridView);        // add view as observer
            this.AddChild(_gridView);       // add view to scene

            /* ************
             * config the static grid controller
             * */
            GridController.SetGridControl(gridModel);   // have to manage this new grid model
            GridController.SetRoundManager(roundModel);   // have to manage this new round model

            /* ************
             * config the rounds
             * */
            RoundController.SetModel(roundModel);
            RoundController.SetView(_roundView);
        }
    }
}
