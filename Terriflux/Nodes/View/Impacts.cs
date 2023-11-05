using Godot;
using System;
using Terriflux.Programs.Gauges;

namespace Terriflux.Programs.Gauges
{
    public partial class Impacts : Control
    {
        private IGauge _social;
        private IGauge _economy;
        private IGauge _ecology;

        public override void _Ready()
        {
            base._Ready();
            ;
            this._social = GetNode<IGauge>("SocialGauge");
            this._economy = GetNode<IGauge>("EconomyGauge");
            this._ecology = GetNode<IGauge>("EcologyGauge");
        }

        public void SetSocial(double newValue)
        {
            _social.SetValue(newValue);
        }

        public void SetEconomy(double newValue)
        {
            _economy.SetValue(newValue);
        }

        public void SetEcology(double newValue)
        {
            _ecology.SetValue(newValue);
        }
    }
}
