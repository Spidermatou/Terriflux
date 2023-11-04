using Godot;
using System.Collections.Generic;


namespace Terriflux.Programs.Gauges
{
    public abstract partial class Gauge : Node2D, IGauge
    {
        private double value;

        protected Gauge()
        {
            this.value = 50; // default
        }

        /// <summary>
        /// Design a Gauge node. 
        /// Remember to add it to your scene to display it.
        /// </summary>
        public abstract Gauge Design();

        public double GetValue()
        {
            return value;
        }

        public void SetValue(double newVal)
        {
            value = newVal;
        }
    }
}