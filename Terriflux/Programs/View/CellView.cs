using Godot;
using System;
using System.Text;
using Terriflux.Programs.Controller;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.View
{
    public partial class CellView : Node2D, IVerbosable
    {
        protected static readonly string defaultTexturePath = OurPaths.TEXTURES + "default" + OurPaths.PNGEXT;
        public static readonly double globalSize = 128;   //px

        // children
        private Label _nicknameLabel;
        public Sprite2D _skin;
        private Button _selectDetector;

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
            _skin = GetNode<Sprite2D>("Skin");
            _selectDetector = GetNode<Button>("SelectDetector");

            // adapt button position to cells
            Vector2 offset = new((float)CellView.GetGlobalSize() / 2,  // calculation of mid-cell location
                (float)CellView.GetGlobalSize() / 2);
            _selectDetector.Position -= offset;     // shift the current position of the button center to that of the cell

            // hide useless
            _nicknameLabel.Hide();

            // default
            ChangeName("Cell");
            ChangeSkin(GD.Load<Texture2D>(defaultTexturePath));
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
            if (_skin == null)
            {
                throw new NullReferenceException(this + "'s skin child not loaded correctly!");
            }
            else if (skin == null)
            {
                throw new ArgumentNullException(nameof(skin));
            }
            else
            {
                _skin.Texture = skin;
                _skin.Scale = new Vector2((float)CellView.GetGlobalSize(), (float)CellView.GetGlobalSize()) / _skin.Texture.GetSize();
            }
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

        // Verbose
        public string Verbose()
        {
            StringBuilder sb = new();
            sb.Append("Cell " + this);
            if (_skin == null)
            {
                sb.Append("_skin null");
            }
            else
            {
                sb.Append("Skin = " + _skin.Texture.ResourceName);
            }
            if (_nicknameLabel == null)
            {
                sb.Append("_nicknameLabel null");
            }
            return sb.ToString();
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
            GridController.SetSelectedCoordinates(this.Position);

            // if all ok: change the cell!
            GridController.StartPlacement();
        }
    }
}