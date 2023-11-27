using Godot;

namespace Terriflux.Programs.TestsZone
{
    public partial class Lab : Node2D
    {
        private Marker2D _spawnMark;
        private Marker2D _spawnMark2;
        private Marker2D _spawnMark3;
        private Marker2D _spawnMark4;

        private Lab() { }

        public override void _Ready()
        {
            // child
            _spawnMark = GetNode<Marker2D>("SpawnMark");
            _spawnMark2 = GetNode<Marker2D>("SpawnMark2");
            _spawnMark3 = GetNode<Marker2D>("SpawnMark3");
            _spawnMark4 = GetNode<Marker2D>("SpawnMark4");

            TestsProvider tp = new(this);
            tp.TInvetoryTable(_spawnMark.Position, true);
        }
    }
}