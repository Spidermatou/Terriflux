using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Gauges
{
    public partial class SociabilityGauge : Gauge
    {
        protected SociabilityGauge() : base() { }

        public override Gauge Design()
        {
            return (SociabilityGauge)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "SociabilityGauge.tscn")
                .Instantiate();
        }
    }
}
