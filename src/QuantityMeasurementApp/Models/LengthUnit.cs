using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The LengthUnit enum represents different units of measurement for length.
    /// Each unit has a conversion factor relative to feet (the base unit).
    /// </summary>
    /// <remarks>
    /// Length units remain enum-based and are adapted to <see cref="IMeasurable"/> through extension methods.
    /// </remarks>
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters,
    }

    /// <summary>
    /// Extension methods for LengthUnit that provide conversion responsibility
    /// to and from the base unit (feet).
    /// </summary>
    /// <remarks>
    /// These extensions form the UC10 bridge from enum values to the common measurement contract.
    /// </remarks>
    public static class LengthUnitExtensions
    {
        private sealed class LengthMeasurable : IMeasurable
        {
            private readonly LengthUnit unit;

            public LengthMeasurable(LengthUnit unit)
            {
                this.unit = unit;
            }

            public double GetConversionFactor()
            {
                return unit.GetConversionFactor();
            }

            public double ConvertToBaseUnit(double value)
            {
                return unit.ConvertToBaseUnit(value);
            }

            public double ConvertFromBaseUnit(double baseValue)
            {
                return unit.ConvertFromBaseUnit(baseValue);
            }

            public string GetUnitName()
            {
                return unit.GetUnitName();
            }
        }

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
        public static double GetConversionFactor(this LengthUnit unit)
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

        /// <summary>
        /// Converts a value in this unit to the base unit (feet).
        /// </summary>
        /// <param name="unit">The source unit.</param>
        /// <param name="value">The value in the source unit.</param>
        /// <returns>The converted value in feet.</returns>
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        /// <summary>
        /// Converts a base-unit (feet) value to this unit.
        /// </summary>
        /// <param name="unit">The target unit.</param>
        /// <param name="baseValue">The value in feet.</param>
        /// <returns>The converted value in the target unit.</returns>
        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }

        public static string GetUnitName(this LengthUnit unit)
        {
            return unit.ToString().ToUpperInvariant();
        }

        public static IMeasurable AsMeasurable(this LengthUnit unit)
        {
            return new LengthMeasurable(unit);
        }
    }
}
