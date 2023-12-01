using Godot;

namespace Terriflux.Programs{
    public partial class RawNode : Node2D
    {
        protected const string PATH_NODES = "Nodes/";
        protected const string PATH_PROGRAMS = "Programs/";
        protected const string PATH_IMAGES = "Ressources/Images/";
        protected RawNode()
        {
            this.Name = this.GetClass().ToString();
            this.Visible = true;
        }

        public override void _Ready()
        {
            base._Ready();
        }
    }
}
