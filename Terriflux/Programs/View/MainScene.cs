using Godot;
using Terriflux.Programs.Controller;
using Terriflux.Programs.Facades;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Round;

namespace Terriflux.Programs.View
{
    public partial class MainScene : Node2D
    {
        private const int gameGridSize = 10;

        private readonly RoundModel roundModel = new();
        private GridModel gridModel = new(gameGridSize);


        // nodes
        private RoundCounter _roundView;
        private Marker2D _markGrid;
        private GridView _gridView;

        private MainScene() : base() { }

        public override void _Ready()
        {
            base._Ready();

            // get children
            _markGrid = GetNode<Marker2D>("GridMark");
            _roundView = GetNode<RoundCounter>("RoundCounter");

            TerritoryManagementFacade territoryManagement = new(this);

            territoryManagement.CreateAndConfigGrid(_markGrid.Position, gameGridSize,
                out gridModel, out this._gridView);

            
            territoryManagement.ConfigRounds(roundModel, _roundView);
        }
    }
}
