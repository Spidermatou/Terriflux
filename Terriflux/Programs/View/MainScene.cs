using Godot;
using Terriflux.Programs.Facades;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Grid;

namespace Terriflux.Programs.View
{
    public partial class MainScene : Node2D
    {
        public int gameGridSize = 10;
        private GridModel gridModel;

        private Marker2D _markGrid;
        private Marker2D _markPlacementList;
        private PlacementList _placementList;
        private GridView _gridView;

        private MainScene() : base() { }

        public override void _Ready()
        {
            base._Ready();

            // child
            _markGrid = GetNode<Marker2D>("GridMark");
            _markPlacementList = GetNode<Marker2D>("PlacementListMark");

            TerritoryManagementFacade territoryManagement = new(this);
            territoryManagement.CreateTerritoryManagerHUD(_markGrid.Position, _markPlacementList.Position, gameGridSize,
                out this._placementList, out this.gridModel, out this._gridView);
        }
    }
}
