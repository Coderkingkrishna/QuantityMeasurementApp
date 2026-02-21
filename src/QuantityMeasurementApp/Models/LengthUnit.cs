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
        Yards,
        Centimeters,
    }

    /// <summary>
    /// Converter class for LengthUnit that provides conversion factor functionality.
    /// </summary>
    public class LengthUnitConverter
    {
        /// <summary>
        /// Gets the conversion factor for a given LengthUnit relative to feet (the base unit).
        /// This method allows for easy conversion of different length units to a common base unit for comparison.
        /// The conversion factors are defined as follows:
        /// - Feet: 1.0 (base unit)
        /// - Inches: 1.0 / 12.0 (1 foot = 12 inches)
        /// - Yards: 3.0 (1 yard = 3 feet)
        /// - Centimeters: 1.0 / 30.48 (1 foot = 30.48 centimeters)
        /// This method throws an ArgumentException if an unsupported unit is provided.
        ///
        /// </summary>
        /// <param name="unit">The unit to get the conversion factor for.</param>
        /// <returns>The conversion factor to convert from the given unit to feet.</returns>
        public double GetConversionFactor(LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                LengthUnit.Yards => 3.0,
                LengthUnit.Centimeters => 1.0 / 30.48,
                _ => throw new ArgumentException($"Unsupported unit: {unit}"),
            };
        }
    }
}
