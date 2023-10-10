using Godot;
using Terriflux.Programs.GameContext;

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
            TestsProvider tp = new(this);
            tp.GridWithBuilds();
        }
    }
}
