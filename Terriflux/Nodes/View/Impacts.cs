using Godot;
using System;
using System.Runtime.CompilerServices;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Gauges
{
    public partial class Impacts : Control
    {
        private IGauge _social;
        private IGauge _economy;
        private IGauge _ecology;

        protected Impacts() { }

        public static Impacts Design()
        {
            return GD.Load<PackedScene>(OurPaths.VIEW_NODES + "Impacts.tscn").Instantiate<Impacts>();
        }

        public override void _Ready()
        {
            base._Ready();

            this._social = GetNode<IGauge>("SocialGauge");
            this._economy = GetNode<IGauge>("EconomyGauge");
            this._ecology = GetNode<IGauge>("EcologyGauge");
        }

        public void SetSocial(double newValue)
        {
            this._social.SetValue(newValue);
        }

        public void SetEconomy(double newValue)
        {
            this._economy.SetValue(newValue);
        }

        public void SetEcology(double newValue)
        {
            this._ecology.SetValue(newValue);
        }
    }
}
