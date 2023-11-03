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

            TestsProvider tp = new(this);
            tp.TRoundModel();
            
            //tp.TRoundController(true);
        }
    }
}