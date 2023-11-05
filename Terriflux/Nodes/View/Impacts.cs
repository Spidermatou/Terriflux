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

        public void AddSocial(double newValue)
        {
            this._social.SetValue(this._social.GetValue() + newValue);
        }

        public void AddEconomy(double newValue)
        {
            this._economy.SetValue(this._economy.GetValue() + newValue);
        }

        public void AddEcology(double newValue)
        {
            this._ecology.SetValue(this._ecology.GetValue() + newValue);
        }
    }
}
