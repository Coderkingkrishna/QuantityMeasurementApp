using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The LengthUnit enum represents different units of measurement for length.
    /// Each unit has a conversion factor relative to feet (the base unit).
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inches,
    }

    /// <summary>
    /// Extension methods for LengthUnit to provide conversion factor functionality.
    /// </summary>
    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Gets the conversion factor for a given LengthUnit relative to feet (base unit).
        /// </summary>
        /// <param name="unit">The unit to get the conversion factor for.</param>
        /// <returns>The conversion factor to convert from the given unit to feet.</returns>
        public static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                _ => throw new ArgumentException($"Unsupported unit: {unit}"),
            };
        }
    }

    public class Length
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        public Length(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}
