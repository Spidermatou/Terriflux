using Godot;
using System;
using System.Linq;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.View;

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
            tp.ImageToolsProvider_OnGrid();
        }
    }
}
