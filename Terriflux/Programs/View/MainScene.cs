using Godot;
using Terriflux.Programs.Facades;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Grid;

namespace Terriflux.Programs.View
{
    public partial class MainScene : Node2D
    {
        public int gameGridSize = 10;

        private Marker2D _markGrid;
        private GridView _gridView;

        private MainScene() : base() { }

        public override void _Ready()
        {
            base._Ready();

            // child
            _markGrid = GetNode<Marker2D>("GridMark");

            GridModel gridModel; 

            TerritoryManagementFacade territoryManagement = new(this);
            territoryManagement.CreateAndConfigGrid(_markGrid.Position, gameGridSize,
                out gridModel, out this._gridView);
        }
    }
}
