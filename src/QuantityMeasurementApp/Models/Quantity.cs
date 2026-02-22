using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The QuantityLength class represents a measurement with a value and a LengthUnit.
    /// It eliminates code duplication by consolidating the functionality of Feet and Inches classes.
    /// It implements the IEquatable interface to provide type-specific equality comparison.
    /// The class supports conversion between different units before comparison, applying the DRY principle.
    /// </summary>
    public sealed class QuantityLength : IEquatable<QuantityLength>
    {
        private const double Epsilon = 1e-9;
        private readonly LengthUnitConverter _converter;

        /// <summary>
        /// Gets the value of the quantity.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets the unit of the quantity.
        /// </summary>
        public LengthUnit Unit { get; }

        /// <summary>
        /// Initializes a new instance of the QuantityLength class with the specified value and unit.
        /// The constructor validates that the value is a finite number and that the unit is a defined LengthUnit.
        /// If the value is not finite or the unit is not supported, an ArgumentException is thrown.
        /// This ensures that the QuantityLength instances are always in a valid state, preventing issues during comparisons and conversions.
        ///</summary>
        public QuantityLength(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.", nameof(value));

            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));

            Value = value;
            Unit = unit;
            _converter = new LengthUnitConverter();
        }

        ///<summary>
        /// Compares this QuantityLength instance with another for equality, supporting cross-unit comparison.
        /// The method converts both measurements to a common base unit (feet) before comparing their values.
        /// </summary>
        public bool Equals(QuantityLength? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            double thisValueInBaseUnit = ConvertToBaseUnit(this.Value, this.Unit);
            double otherValueInBaseUnit = ConvertToBaseUnit(other.Value, other.Unit);

            return Math.Abs(thisValueInBaseUnit - otherValueInBaseUnit) < Epsilon;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not QuantityLength other)
                return false;

            return Equals(other);
        }

        ///<summary>
        /// Returns the hash code for this instance.
        /// The hash code is based on the value converted to the base unit (feet).
        /// </summary>
        public override int GetHashCode()
        {
            return ConvertToBaseUnit(Value, Unit).GetHashCode();
        }

        ///<summary>
        /// Converts the quantity to the specified target unit.
        /// The method first converts the value to the base unit (feet) and then converts it to the target unit using the conversion factor from the LengthUnitConverter.
        /// </summary>
        public double ConvertTo(LengthUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit));

            double valueInBaseUnit = ConvertToBaseUnit(Value, Unit);
            double targetConversionFactor = _converter.GetConversionFactor(targetUnit);

            return valueInBaseUnit / targetConversionFactor;
        }

        ///<summary>
        /// Converts the quantity to a new QuantityLength instance with the specified target unit.
        /// </summary>
        public QuantityLength ConvertToQuantity(LengthUnit targetUnit)
        {
            return new QuantityLength(ConvertTo(targetUnit), targetUnit);
        }

        ///<summary>
        /// Adds another QuantityLength to this instance, supporting cross-unit addition.
        /// The method converts the other measurement to the same unit as this instance, performs the addition, and returns a new QuantityLength instance with the result.
        /// </summary>
        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            double sumInCurrentUnit = Value + other.ConvertTo(Unit);
            return new QuantityLength(sumInCurrentUnit, Unit);
        }

        ///<summary>
        /// Adds a measurement specified by a value and unit to this instance, supporting cross-unit addition
        /// The method creates a QuantityLength instance for the other measurement, performs the addition using the Add method, and returns a new QuantityLength instance with the result.
        /// </summary>
        public QuantityLength Add(double value, LengthUnit unit)
        {
            var other = new QuantityLength(value, unit);
            return Add(other);
        }

        ///<summary>
        /// Helper method to convert a value from a specified unit to the base unit (feet).
        /// </summary>
        private double ConvertToBaseUnit(double value, LengthUnit unit)
        {
            double conversionFactor = _converter.GetConversionFactor(unit);
            return value * conversionFactor;
        }

        ///<summary>
        /// Compares this QuantityLength instance with another for equality, supporting cross-unit comparison.
        /// The method converts both measurements to a common base unit (feet) before comparing their values.
        /// </summary>
        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}
