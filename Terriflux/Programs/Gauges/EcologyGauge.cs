using Godot;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Gauges
{
    public partial class EcologyGauge : Gauge
    {
        protected EcologyGauge() : base() { }

        public override Gauge Design()
        {
            return (EcologyGauge)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "EcologyGauge.tscn")
                .Instantiate();
        }
    }
}