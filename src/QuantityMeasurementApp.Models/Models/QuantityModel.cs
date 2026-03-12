namespace QuantityMeasurementApp.Models.Models
{
    public class QuantityModel<TUnit>
    {
        public double Value { get; }
        public TUnit Unit { get; }

        public QuantityModel(double value, TUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}
