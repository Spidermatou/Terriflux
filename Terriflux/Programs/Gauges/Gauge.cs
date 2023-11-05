using Godot;
using System.Collections.Generic;


namespace Terriflux.Programs.Gauges
{
    public abstract partial class Gauge : TextureProgressBar, IGauge
    {
        protected Gauge()
        {
            this.SetValue(50); // default
        }

        /// <summary>
        /// Design a Gauge node. 
        /// Remember to add it to your scene to display it.
        /// </summary>
        public abstract Gauge Design();

        public double GetValue()
        {
            return Value;
        }

        public void SetValue(double newVal)
        {
            this.Value = newVal;
        }
    }
}