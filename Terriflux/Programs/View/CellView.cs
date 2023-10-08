using Godot;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
    public partial class CellView : Node2D, ICellObserver, IVerbosable 
    {
        // children
        private Label _nicknameLabel;
        private Sprite2D _skin;

        // Creation
        /// <summary>
        /// Simple class construction not allowed. Please use the associated Design() function.
        /// </summary>
        private CellView() { }

        public static CellView Design()
        {
            return (CellView)GD.Load<PackedScene>(Paths.VIEW_NODES + "CellView" + Paths.GDEXT)
                .Instantiate();
        }

        public override void _Ready()
        {
            _nicknameLabel = GetNode<Label>("NicknameLabel");
            _skin = GetNode<Sprite2D>("Skin");

            // Hide useless
            _nicknameLabel.Hide();

            // default
            _nicknameLabel.Text = "Cell";
        }

        // Skin
        /// <summary>
        /// Changes the image representing this cell on the screen
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void ChangeSkin(string path)
        {
            if (this._skin == null)
            {
                throw new NullReferenceException(this + "'s skin child not loaded correctly!");
            }

            if (File.Exists(path))
            {
                this._skin.Texture = GD.Load<Texture2D>(path);
            }
            else
            {
                throw new ArgumentException($"Invalid texture path '{path}'.");
            }
        }

        /// <summary>
        /// Changes the image representing this cell on the screen
        /// </summary>
        /// <param name="skin"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ChangeSkin(Texture2D skin)
        {
            if (this._skin == null)
            {
                throw new NullReferenceException(this + "'s skin child not loaded correctly!");
            }
            else if (skin == null)
            {
                throw new ArgumentNullException(nameof(skin));
            }
            else
            {
                this._skin.Texture = skin;
            }
        }

        /// <summary>
        /// Changes the image representing this cell on the screen
        /// </summary>
        /// <param name="skin"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ChangeSkin(Sprite2D skin)
        {
            if (this._skin == null)
            {
                throw new NullReferenceException(this + "'s skin child not loaded correctly!");
            }
            else if (skin == null)
            {
                throw new ArgumentNullException(nameof(skin));
            }
            else if (skin.Texture == null)
            {
                throw new ArgumentNullException("This skin doesn't have texture!");
            }
            else
            {
                this._skin.Texture = skin.Texture;
            }
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

        public void UpdatePlacement(Vector2 coordinates)
        {
            this.Position = coordinates;
        }

        public void UpdateCellName(string cname)
        {
            this._nicknameLabel.Text = cname;
        }

        public void UpdateCellKind(CellKind kind)
        {
            // Does nothing
        }
    }
}