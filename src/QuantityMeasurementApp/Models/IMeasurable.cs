namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Common unit contract for UC10 generic quantity operations.
    /// Implementations define conversion to/from a category base unit.
    /// </summary>
    public interface IMeasurable
    {
        double GetConversionFactor();

        double ConvertToBaseUnit(double value);

        double ConvertFromBaseUnit(double baseValue);

        string GetUnitName();
    }
}
