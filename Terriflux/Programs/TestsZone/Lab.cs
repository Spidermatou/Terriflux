using Godot;

namespace Terriflux.Programs.TestsZone
{
    public partial class Lab : Node2D
    {
        private Marker2D _spawnMark;
        private Marker2D _spawnMark2;

        private Lab() { }

        public override void _Ready()
        {
            // child
            _spawnMark = GetNode<Marker2D>("SpawnMark");
            _spawnMark2 = GetNode<Marker2D>("SpawnMark2");

            this.Rounds();
        }

        // latest tests
        private void Rounds()
        {
            TestsProvider tp = new(this);
            tp.TRoundView(true);
        }

        // old tests
        private void TerritoryManagement()
        {
            TestsProvider tp = new(this);

            // Models
            GD.Print("--Test cell model--");
            TestsProvider.TCellModel();

            GD.Print("--Test grass model--");
            TestsProvider.TGrassModel();

            GD.Print("--Test building model--");
            TestsProvider.TBuildingModel();

            GD.Print("--Test grid model--");
            TestsProvider.TGridModel();

            // Views
            GD.Print("--Test cell view--");
            tp.TCellView();

            GD.Print("--Test grass view--");
            tp.TGrassView();

            GD.Print("--Test building view--");
            tp.TBuildingView();

            // Factories
            GD.Print("--Test building factory--");
            tp.TBuildingFactory();

            GD.Print("--Test building factory - version: no texture provided--");
            tp.TBuildingFactory_WithUnprovidedTexture();

            GD.Print("--Test grid factory - version: grass only--");
            tp.TGridFactory_GrassOnly(_spawnMark.Position);

            GD.Print("--Test grid factory - version: buildings classic--");
            tp.TGridFactory_WithBuildings(_spawnMark.Position);
        }

        private void HUD()
        {
            TestsProvider tp = new(this);
            GD.Print("--Test placement list view --");
            tp.TPlacementListView(_spawnMark.Position);

            GD.Print("--Test clickable grid --");
            tp.TClickableGridView(_spawnMark.Position, _spawnMark2.Position, true);
        }
    }
}