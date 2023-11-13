using Godot;
using System;
using System.Text;
using Terriflux.Programs.Controller;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.View
{
    public partial class CellView : TextureButton
    {
        protected static readonly string defaultTexturePath = OurPaths.TEXTURES + "default" + OurPaths.PNGEXT;
        public static readonly double globalSize = 128;   //px

        // children
        private Label _nicknameLabel;

        // Creation
        /// <summary>
        /// Create a view for any kind of cell.
        /// Careful: Simple class construction not allowed. Please use the associated Design() function!
        /// </summary>
        protected CellView() { }

        public override void _Ready()
        {
            base._Ready();
            _nicknameLabel = GetNode<Label>("NicknameLabel");

            // hide useless
            _nicknameLabel.Hide();

            // default
            ChangeName("Cell");
            ChangeSkin(GD.Load<Texture2D>(defaultTexturePath));
            this.TextureHover = GD.Load<Texture2D>(OurPaths.TEXTURES + "grass.png");
        }

        /// <summary>
        /// Design a CellView. 
        /// Remember to add it to your scene to display it!
        /// </summary>
        /// <returns></returns>
        public static CellView Design()
        {
            return (CellView)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "CellView" + OurPaths.NODEXT)
                .Instantiate();
        }

        // Skin
        /// <summary>
        /// Changes the image representing this cell on the screen
        /// </summary>
        /// <param name="skin"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void ChangeSkin(Texture2D skin)
        {
            this.TextureNormal = skin;
        }

        protected void ChangeName(string name)
        {
            if (_nicknameLabel != null)
            {
                _nicknameLabel.Text = name;
            }
        }

        /// <returns> The theoretic size of any cell.</returns>
        public static double GetGlobalSize()
        {
            return globalSize;
        }

        // Events
        private void OnMouseOver()
        {
            _nicknameLabel.Show();
        }

        private void OnMouseExit()
        {
            _nicknameLabel.Hide();
        }

        private void OnSelectDetectorPressed()
        {
            GridController.SetSelectedCoordinates(Position);

            // if all ok: change the cell!
            GridController.StartPlacement();
        }
    }
}