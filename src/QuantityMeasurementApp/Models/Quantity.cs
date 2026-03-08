using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Internal abstraction used to compare quantities across generic instances by base-unit value and unit category.
    /// </summary>
    internal interface IQuantityComparable
    {
        Type UnitType { get; }

        double BaseValue { get; }
    }

    /// <summary>
    /// Resolves supported unit enums to their <see cref="IMeasurable"/> adapters.
    /// </summary>
    internal static class MeasurableResolver
    {
        public static bool TryResolve<U>(U unit, out IMeasurable measurable)
            where U : struct, Enum
        {
            measurable = default!;

            if (!Enum.IsDefined(typeof(U), unit))
            {
                return false;
            }

            if (unit is LengthUnit lengthUnit)
            {
                measurable = lengthUnit.AsMeasurable();
                return true;
            }

            if (unit is WeightUnit weightUnit)
            {
                measurable = weightUnit.AsMeasurable();
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Generic immutable quantity model for UC10.
    /// Centralizes equality, conversion, and addition logic for any supported unit category.
    /// </summary>
    public sealed class Quantity<U> : IEquatable<Quantity<U>>, IQuantityComparable
        where U : struct, Enum
    {
        private const double Epsilon = 1e-6;

        /// <summary>
        /// Gets the value of the quantity.
        /// </summary>
        public double Value { get; }

        /// <summary>
        public U Unit { get; }

        /// <summary>
        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.", nameof(value));

            if (!MeasurableResolver.TryResolve(unit, out _))
                throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));

            Value = value;
            Unit = unit;
        }

        private IMeasurable ResolveMeasurable(U unit)
        {
            if (!MeasurableResolver.TryResolve(unit, out var measurable))
                throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));

            return measurable;
        }

        public bool Equals(Quantity<U>? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return Equals((object)other);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not IQuantityComparable other)
                return false;

            if (Unit.GetType() != other.UnitType)
                return false;

            return Math.Abs(BaseValue - other.BaseValue) < Epsilon;
        }

        public override int GetHashCode()
        {
            long normalized = (long)Math.Round(BaseValue / Epsilon, MidpointRounding.AwayFromZero);
            return HashCode.Combine(Unit.GetType(), normalized);
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (!MeasurableResolver.TryResolve(targetUnit, out var targetMeasurable))
                throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit));

            double valueInBaseUnit = ResolveMeasurable(Unit).ConvertToBaseUnit(Value);
            double convertedValue = targetMeasurable.ConvertFromBaseUnit(valueInBaseUnit);

            return new Quantity<U>(Math.Round(convertedValue, 2), targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return Add(other, Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (!MeasurableResolver.TryResolve(targetUnit, out var targetMeasurable))
                throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit));

            double thisValueInBase = ResolveMeasurable(Unit).ConvertToBaseUnit(Value);
            double otherValueInBase = other
                .ResolveMeasurable(other.Unit)
                .ConvertToBaseUnit(other.Value);
            double sumInTargetUnit = targetMeasurable.ConvertFromBaseUnit(
                thisValueInBase + otherValueInBase
            );

            return new Quantity<U>(Math.Round(sumInTargetUnit, 2), targetUnit);
        }

        public Quantity<U> Add(double value, U unit)
        {
            var other = new Quantity<U>(value, unit);
            return Add(other);
        }

        public Quantity<U> Add(double value, U unit, U targetUnit)
        {
            var other = new Quantity<U>(value, unit);
            return Add(other, targetUnit);
        }

        Type IQuantityComparable.UnitType => Unit.GetType();

        double IQuantityComparable.BaseValue => BaseValue;

        private double BaseValue
        {
            get { return ResolveMeasurable(Unit).ConvertToBaseUnit(Value); }
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}
