using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The WeightUnit enum represents different units of measurement for weight.
    /// Each unit has a conversion factor relative to kilogram (the base unit).
    /// </summary>
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound,
    }

    /// <summary>
    /// Extension methods for WeightUnit that provide conversion responsibility
    /// to and from the base unit (kilogram).
    /// </summary>
    public static class WeightUnitExtensions
    {
        /// <summary>
        /// Gets the conversion factor for a given WeightUnit relative to kilogram (the base unit).
        /// </summary>
        public static double GetConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.Kilogram => 1.0,
                WeightUnit.Gram => 0.001,
                WeightUnit.Pound => 0.453592,
                _ => throw new ArgumentException($"Unsupported unit: {unit}"),
            };
        }

        /// <summary>
        /// Converts a value in this unit to the base unit (kilogram).
        /// </summary>
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        /// <summary>
        /// Converts a base-unit (kilogram) value to this unit.
        /// </summary>
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }
    }
}