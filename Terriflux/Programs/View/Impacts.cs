using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Gauges
{
    public partial class Impacts : Control
    {
        private IGauge _social;
        private IGauge _economy;
        private IGauge _ecology;
        private Button _inventoryButton;
        private Sprite2D _inventoryIcon;
        private Sprite2D _inventoryEmblem;

        private IIventory _inventory; 

        private readonly Texture2D _inventoryIconTextureNormal = GD.Load<Texture2D>(OurPaths.ICONS + "inventorysymbol.png");
        private readonly Texture2D _inventoryIconTextureHover = GD.Load<Texture2D>(OurPaths.ICONS + "leftclick.png");
        private readonly Vector2 _iconScaleClosed = new((float)0.23, (float)0.295);
        private readonly Vector2 _iconScaleOpened = new((float)0.15, (float)0.178);

        protected Impacts() { }

        public static Impacts Design()
        {
            return GD.Load<PackedScene>(OurPaths.VIEW_NODES + "Impacts.tscn").Instantiate<Impacts>();
        }

        public override void _Ready()
        {
            base._Ready();

            // get children
            _social = GetNode<IGauge>("SocialGauge");
            _economy = GetNode<IGauge>("EconomyGauge");
            _ecology = GetNode<IGauge>("EcologyGauge");
            _inventoryButton = GetNode<Button>("InventoryButton");
            _inventoryIcon = GetNode<Sprite2D>("InventoryButton/Icon"); 
            _inventoryEmblem = GetNode<Sprite2D>("InventoryButton/Emblem");

            // default textures
            this._inventoryIcon.Texture = this._inventoryIconTextureNormal;

            _inventory = GetNode<IIventory>("Inventory"); 
            _inventory.Hide();
        }

        public void AddSocial(double newValue)
        {
            _social.SetValue(_social.GetValue() + newValue);
        }

        public void AddEconomy(double newValue)
        {
            _economy.SetValue(_economy.GetValue() + newValue);
        }

        public void AddEcology(double newValue)
        {
            _ecology.SetValue(_ecology.GetValue() + newValue);
        }

        /// <returns>Social, Economy, Ecology</returns>
        public double[] GetImpacts()
        {
            return new double[] { _social.GetValue(), _economy.GetValue(), _ecology.GetValue() };
        }

        // Feedback
        private void OnInventoryButtonPressed()
        {
            if (_inventory != null)
            {
                // open inventory if is not
                if (!this._inventory.IsVisibleInTree())
                {
                    this._inventory.Show();

                    // feedback
                    this._inventoryEmblem.FlipV = true;
                    this._inventoryIcon.Scale = this._iconScaleClosed;
                    this._inventoryButton.FocusMode = FocusModeEnum.None;
                }
                else // already opened?
                {
                    this._inventory.Hide();

                    // feedback
                    this._inventoryEmblem.FlipV = false;
                    this._inventoryIcon.Scale = this._iconScaleOpened;
                    this._inventoryButton.FocusMode = FocusModeEnum.None;
                }
            }
        }

        private void OnInventoryButtonMouseEntered()
        {
            this._inventoryIcon.Texture = this._inventoryIconTextureHover;
        }

        private void OnInventoryButtonMouseExited()
        {
            this._inventoryIcon.Texture = this._inventoryIconTextureNormal;
        }
    }
}
