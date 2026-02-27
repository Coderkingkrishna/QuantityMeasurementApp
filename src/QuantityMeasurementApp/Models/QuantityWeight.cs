using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The QuantityWeight class represents a measurement with a value and a WeightUnit.
    /// </summary>
    public sealed class QuantityWeight : IEquatable<QuantityWeight>
    {
        private const double Epsilon = 1e-6;

        /// <summary>
        /// Gets the value of the quantity.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets the unit of the quantity.
        /// </summary>
        public WeightUnit Unit { get; }

        /// <summary>
        /// Initializes a new instance of the QuantityWeight class with the specified value and unit.
        /// </summary>
        public QuantityWeight(double value, WeightUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.", nameof(value));

            if (!Enum.IsDefined(typeof(WeightUnit), unit))
                throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Compares this QuantityWeight instance with another for equality, supporting cross-unit comparison.
        /// </summary>
        public bool Equals(QuantityWeight? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            double thisValueInBaseUnit = Unit.ConvertToBaseUnit(Value);
            double otherValueInBaseUnit = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(thisValueInBaseUnit - otherValueInBaseUnit) < Epsilon;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not QuantityWeight other)
                return false;

            return Equals(other);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            double valueInBaseUnit = Unit.ConvertToBaseUnit(Value);
            long normalized = (long)Math.Round(valueInBaseUnit / Epsilon, MidpointRounding.AwayFromZero);
            return HashCode.Combine(normalized);
        }

        /// <summary>
        /// Converts the quantity to the specified target unit.
        /// </summary>
        public double ConvertTo(WeightUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit));

            double valueInBaseUnit = Unit.ConvertToBaseUnit(Value);
            return targetUnit.ConvertFromBaseUnit(valueInBaseUnit);
        }

        /// <summary>
        /// Converts the quantity to a new QuantityWeight instance with the specified target unit.
        /// </summary>
        public QuantityWeight ConvertToQuantity(WeightUnit targetUnit)
        {
            return new QuantityWeight(ConvertTo(targetUnit), targetUnit);
        }

        /// <summary>
        /// Adds another QuantityWeight to this instance.
        /// </summary>
        public QuantityWeight Add(QuantityWeight other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return Add(other, Unit);
        }

        /// <summary>
        /// Adds another QuantityWeight to this instance and returns result in target unit.
        /// </summary>
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit));

            double sumInBaseUnit = Unit.ConvertToBaseUnit(Value) + other.Unit.ConvertToBaseUnit(other.Value);
            double sumInTargetUnit = targetUnit.ConvertFromBaseUnit(sumInBaseUnit);
            return new QuantityWeight(sumInTargetUnit, targetUnit);
        }

        /// <summary>
        /// Adds a measurement specified by a value and unit to this instance.
        /// </summary>
        public QuantityWeight Add(double value, WeightUnit unit)
        {
            var other = new QuantityWeight(value, unit);
            return Add(other);
        }

        /// <summary>
        /// Adds a measurement specified by a value and unit to this instance and returns result in target unit.
        /// </summary>
        public QuantityWeight Add(double value, WeightUnit unit, WeightUnit targetUnit)
        {
            var other = new QuantityWeight(value, unit);
            return Add(other, targetUnit);
        }

        /// <summary>
        /// Returns a string representation of the quantity.
        /// </summary>
        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}