using Godot;
using System;

namespace Terriflux.Programs.View
{
    public partial class StandardWindow : Node2D
    {
        private Label _title;

        /***************
         * READY */
        public override void _Ready()
        {
            _title = GetNode<Label>("Title");
        }

        /***************
         * GETSET */
        public void SetTitle(string newTitle)
        {
            _title.Text = newTitle;
        }

        /***************
         * EVENT */
        private void OnExitPressed()
        {
            this.Hide();
        }
    }
}