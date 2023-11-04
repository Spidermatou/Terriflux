using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Gauges
{
    public partial class EconomyGauge : Gauge
    {
        protected EconomyGauge() : base() { }

        public override Gauge Design()
        {
            return (EconomyGauge)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "EconomyGauge.tscn")
                .Instantiate();
        }
    }
}