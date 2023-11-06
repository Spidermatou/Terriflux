namespace Terriflux.Programs.Gauges
{
    public interface IGauge
    {
        double GetValue();
        void SetValue(double newVal);
    }
}