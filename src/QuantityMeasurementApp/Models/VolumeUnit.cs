using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The VolumeUnit enum represents different units of measurement for volume.
    /// Each unit has a conversion factor relative to litre (the base unit).
    /// </summary>
    public enum VolumeUnit
    {
        Litre,
        Millilitre,
        Gallon,
    }

    /// <summary>
    /// Extension methods for VolumeUnit that provide conversion responsibility
    /// to and from the base unit (litre).
    /// </summary>
    public static class VolumeUnitExtensions
    {
        private sealed class VolumeMeasurable : IMeasurable
        {
            private readonly VolumeUnit unit;

            public VolumeMeasurable(VolumeUnit unit)
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
        /// Gets the conversion factor for a given VolumeUnit relative to litre (the base unit).
        /// </summary>
        public static double GetConversionFactor(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.Litre => 1.0,
                VolumeUnit.Millilitre => 0.001,
                VolumeUnit.Gallon => 3.78541,
                _ => throw new ArgumentException($"Unsupported unit: {unit}"),
            };
        }

        /// <summary>
        /// Converts a value in this unit to the base unit (litre).
        /// </summary>
        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        /// <summary>
        /// Converts a base-unit (litre) value to this unit.
        /// </summary>
        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }

        public static string GetUnitName(this VolumeUnit unit)
        {
            return unit.ToString().ToUpperInvariant();
        }

        public static IMeasurable AsMeasurable(this VolumeUnit unit)
        {
            return new VolumeMeasurable(unit);
        }
    }
}
