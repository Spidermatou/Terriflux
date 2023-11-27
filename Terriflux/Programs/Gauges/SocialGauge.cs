using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Gauges
{
    public partial class SocialGauge : Gauge
    {
        protected SocialGauge() : base() { }

        public override Gauge Design()
        {
            return (SocialGauge)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "SociabilityGauge.tscn")
                .Instantiate();
        }
    }
}
