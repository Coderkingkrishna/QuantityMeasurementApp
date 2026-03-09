using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Represents supported temperature units with Celsius as the base unit for conversion.
    /// </summary>
    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
        Kelvin,
    }

    /// <summary>
    /// Extension methods for <see cref="TemperatureUnit"/> that bridge enum units to <see cref="IMeasurable"/>.
    /// </summary>
    public static class TemperatureUnitExtensions
    {
        private sealed class TemperatureMeasurable : IMeasurable
        {
            private readonly TemperatureUnit unit;

            public TemperatureMeasurable(TemperatureUnit unit)
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

            public bool SupportsArithmetic()
            {
                SupportsArithmetic supportsArithmetic = () => false;
                return supportsArithmetic();
            }

            public void ValidateOperationSupport(string operation)
            {
                throw new NotSupportedException(
                    $"Temperature does not support {operation} operation for absolute values."
                );
            }
        }

        /// <summary>
        /// Returns a nominal conversion factor for temperature units.
        /// </summary>
        /// <remarks>
        /// Temperature conversions are non-linear; this value is informational and not used for conversion formulas.
        /// </remarks>
        public static double GetConversionFactor(this TemperatureUnit unit)
        {
            return 1.0;
        }

        /// <summary>
        /// Converts a temperature value to Celsius base-unit value.
        /// </summary>
        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            Func<double, double> conversion = unit switch
            {
                TemperatureUnit.Celsius => celsius => celsius,
                TemperatureUnit.Fahrenheit => fahrenheit => (fahrenheit - 32.0) * 5.0 / 9.0,
                TemperatureUnit.Kelvin => kelvin => kelvin - 273.15,
                _ => throw new ArgumentException($"Unsupported unit: {unit}"),
            };

            return conversion(value);
        }

        /// <summary>
        /// Converts a Celsius base-unit value to the target temperature unit.
        /// </summary>
        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            Func<double, double> conversion = unit switch
            {
                TemperatureUnit.Celsius => celsius => celsius,
                TemperatureUnit.Fahrenheit => celsius => (celsius * 9.0 / 5.0) + 32.0,
                TemperatureUnit.Kelvin => celsius => celsius + 273.15,
                _ => throw new ArgumentException($"Unsupported unit: {unit}"),
            };

            return conversion(baseValue);
        }

        /// <summary>
        /// Gets uppercase unit name.
        /// </summary>
        public static string GetUnitName(this TemperatureUnit unit)
        {
            return unit.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Wraps temperature enum into measurable adapter.
        /// </summary>
        public static IMeasurable AsMeasurable(this TemperatureUnit unit)
        {
            return new TemperatureMeasurable(unit);
        }
    }
}
