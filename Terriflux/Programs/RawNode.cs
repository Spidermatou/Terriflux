using Godot;
using System.Text;

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

        /// <summary>
        /// Provides additional information about the node.
        /// </summary>
        /// <returns></returns>
        public virtual string Verbose()
        {
            StringBuilder sb = new();
            sb.Append($"Name: {this.Name}")
                .Append($"Class: {this.GetClass()}")
                .Append($"Visible on screen: {this.Visible}");
            return sb.ToString();
        }
    }
}
