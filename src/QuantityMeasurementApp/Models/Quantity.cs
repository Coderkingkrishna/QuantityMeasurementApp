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
        /// <summary>
        /// Tries to resolve a supported enum unit value to an <see cref="IMeasurable"/> adapter.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="unit">The unit value to resolve.</param>
        /// <param name="measurable">The resolved measurable adapter when successful.</param>
        /// <returns>True when the unit is defined and supported; otherwise false.</returns>
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

            if (unit is VolumeUnit volumeUnit)
            {
                measurable = volumeUnit.AsMeasurable();
                return true;
            }

            if (unit is TemperatureUnit temperatureUnit)
            {
                measurable = temperatureUnit.AsMeasurable();
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
        /// Supported arithmetic operations for centralized base-unit computation.
        /// </summary>
        private enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide,
        }

        /// <summary>
        /// Gets the value of the quantity.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets the unit of the quantity.
        /// </summary>
        public U Unit { get; }

        /// <summary>
        /// Initializes a new immutable quantity value for the given unit.
        /// </summary>
        /// <param name="value">The numeric quantity value.</param>
        /// <param name="unit">The unit associated with the value.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the value is not finite or the unit is unsupported.
        /// </exception>
        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.", nameof(value));

            if (!MeasurableResolver.TryResolve(unit, out _))
                throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Resolves a supported unit to its measurable adapter.
        /// </summary>
        /// <param name="unit">The unit to resolve.</param>
        /// <returns>The measurable adapter for conversion operations.</returns>
        /// <exception cref="ArgumentException">Thrown when the unit is unsupported.</exception>
        private IMeasurable ResolveMeasurable(U unit)
        {
            if (!MeasurableResolver.TryResolve(unit, out var measurable))
                throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));

            return measurable;
        }

        /// <summary>
        /// Compares this quantity with another quantity of the same generic type for equality.
        /// </summary>
        /// <param name="other">The other quantity to compare.</param>
        /// <returns>True when values are equal in base unit within tolerance; otherwise false.</returns>
        public bool Equals(Quantity<U>? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return Equals((object)other);
        }

        /// <summary>
        /// Compares this quantity with another object that can expose comparable quantity metadata.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True when categories match and base-unit values are equal within tolerance.</returns>
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

        /// <summary>
        /// Returns a hash code consistent with epsilon-based equality on base-unit value.
        /// </summary>
        /// <returns>A hash code for this quantity.</returns>
        public override int GetHashCode()
        {
            long normalized = (long)Math.Round(BaseValue / Epsilon, MidpointRounding.AwayFromZero);
            return HashCode.Combine(Unit.GetType(), normalized);
        }

        /// <summary>
        /// Converts this quantity to a target unit in the same category.
        /// </summary>
        /// <param name="targetUnit">The unit to convert to.</param>
        /// <returns>A new quantity expressed in the target unit.</returns>
        /// <exception cref="ArgumentException">Thrown when the target unit is unsupported.</exception>
        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (!MeasurableResolver.TryResolve(targetUnit, out var targetMeasurable))
                throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit));

            double valueInBaseUnit = ResolveMeasurable(Unit).ConvertToBaseUnit(Value);
            double convertedValue = targetMeasurable.ConvertFromBaseUnit(valueInBaseUnit);

            return new Quantity<U>(Math.Round(convertedValue, 2), targetUnit);
        }

        /// <summary>
        /// Adds another quantity and returns the result in this quantity's unit.
        /// </summary>
        /// <param name="other">The quantity to add.</param>
        /// <returns>A new quantity containing the sum.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="other"/> is null.</exception>
        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, Unit);
        }

        /// <summary>
        /// Adds another quantity and returns the result in an explicit target unit.
        /// </summary>
        /// <param name="other">The quantity to add.</param>
        /// <param name="targetUnit">The unit for the returned result.</param>
        /// <returns>A new quantity containing the sum in <paramref name="targetUnit"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="other"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the target unit is unsupported.</exception>
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(
                other,
                targetUnit,
                targetUnitRequired: true,
                operation: ArithmeticOperation.Add
            );

            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            return ConvertBaseResultToQuantity(baseResult, targetUnit);
        }

        /// <summary>
        /// Adds a primitive value/unit pair to this quantity in this quantity's unit.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <param name="unit">The unit of the value to add.</param>
        /// <returns>A new quantity containing the sum.</returns>
        public Quantity<U> Add(double value, U unit)
        {
            var other = new Quantity<U>(value, unit);
            return Add(other);
        }

        /// <summary>
        /// Adds a primitive value/unit pair and returns the result in an explicit target unit.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <param name="unit">The unit of the value to add.</param>
        /// <param name="targetUnit">The target unit of the result.</param>
        /// <returns>A new quantity containing the sum in <paramref name="targetUnit"/>.</returns>
        public Quantity<U> Add(double value, U unit, U targetUnit)
        {
            var other = new Quantity<U>(value, unit);
            return Add(other, targetUnit);
        }

        /// <summary>
        /// Subtracts another quantity and returns the result in this quantity's unit.
        /// </summary>
        /// <param name="other">The quantity to subtract.</param>
        /// <returns>A new quantity containing the difference.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="other"/> is null.</exception>
        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, Unit);
        }

        /// <summary>
        /// Subtracts another quantity and returns the result in an explicit target unit.
        /// </summary>
        /// <param name="other">The quantity to subtract.</param>
        /// <param name="targetUnit">The unit for the returned result.</param>
        /// <returns>A new quantity containing the difference in <paramref name="targetUnit"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="other"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when target unit is unsupported.</exception>
        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(
                other,
                targetUnit,
                targetUnitRequired: true,
                operation: ArithmeticOperation.Subtract
            );

            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            return ConvertBaseResultToQuantity(baseResult, targetUnit);
        }

        /// <summary>
        /// Divides this quantity by another quantity and returns a dimensionless ratio.
        /// </summary>
        /// <param name="other">The divisor quantity.</param>
        /// <returns>The scalar ratio in base-unit terms.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="other"/> is null.</exception>
        /// <exception cref="ArithmeticException">Thrown when divisor resolves to zero.</exception>
        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(
                other,
                null,
                targetUnitRequired: false,
                operation: ArithmeticOperation.Divide
            );
            return PerformBaseArithmetic(other, ArithmeticOperation.Divide);
        }

        /// <summary>
        /// Validates arithmetic operands and optional target unit for centralized DRY enforcement.
        /// </summary>
        /// <param name="other">The secondary arithmetic operand.</param>
        /// <param name="targetUnit">Optional target unit for result projection.</param>
        /// <param name="targetUnitRequired">True when a target unit must be validated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="other"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when values are invalid or target unit is unsupported.</exception>
        private void ValidateArithmeticOperands(
            Quantity<U> other,
            U? targetUnit,
            bool targetUnitRequired,
            ArithmeticOperation operation
        )
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (Unit.GetType() != other.Unit.GetType())
                throw new ArgumentException(
                    "Quantities must belong to the same measurement category.",
                    nameof(other)
                );

            ValidateFiniteQuantity(this, nameof(Value));
            ValidateFiniteQuantity(other, nameof(other));

            ResolveMeasurable(Unit).ValidateOperationSupport(operation.ToString());
            other.ResolveMeasurable(other.Unit).ValidateOperationSupport(operation.ToString());

            if (!targetUnitRequired)
                return;

            if (!targetUnit.HasValue)
                throw new ArgumentException("Target unit is required.", nameof(targetUnit));

            if (!MeasurableResolver.TryResolve(targetUnit.Value, out _))
                throw new ArgumentException(
                    $"Unsupported unit: {targetUnit.Value}",
                    nameof(targetUnit)
                );
        }

        /// <summary>
        /// Performs arithmetic in base-unit space for the provided operation.
        /// </summary>
        /// <param name="other">The secondary arithmetic operand.</param>
        /// <param name="operation">The arithmetic operation to execute.</param>
        /// <returns>The arithmetic result in base unit for add/subtract, or scalar for divide.</returns>
        /// <exception cref="ArithmeticException">Thrown when dividing by zero quantity.</exception>
        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            double thisValueInBase = ResolveMeasurable(Unit).ConvertToBaseUnit(Value);
            double otherValueInBase = other
                .ResolveMeasurable(other.Unit)
                .ConvertToBaseUnit(other.Value);

            return operation switch
            {
                ArithmeticOperation.Add => thisValueInBase + otherValueInBase,
                ArithmeticOperation.Subtract => thisValueInBase - otherValueInBase,
                ArithmeticOperation.Divide => DivideBaseValues(thisValueInBase, otherValueInBase),
                _ => throw new InvalidOperationException($"Unsupported operation: {operation}"),
            };
        }

        /// <summary>
        /// Converts a base-unit arithmetic result to the specified target unit and creates a new quantity.
        /// </summary>
        /// <param name="baseResult">The arithmetic result in base unit.</param>
        /// <param name="targetUnit">The target unit to project to.</param>
        /// <returns>A rounded quantity in the target unit.</returns>
        private Quantity<U> ConvertBaseResultToQuantity(double baseResult, U targetUnit)
        {
            double convertedValue = ResolveMeasurable(targetUnit).ConvertFromBaseUnit(baseResult);
            return new Quantity<U>(Math.Round(convertedValue, 2), targetUnit);
        }

        /// <summary>
        /// Divides two base-unit values and enforces non-zero divisor semantics.
        /// </summary>
        /// <param name="dividend">The base-unit dividend.</param>
        /// <param name="divisor">The base-unit divisor.</param>
        /// <returns>The scalar quotient.</returns>
        /// <exception cref="ArithmeticException">Thrown when divisor is zero.</exception>
        private static double DivideBaseValues(double dividend, double divisor)
        {
            if (Math.Abs(divisor) < Epsilon)
                throw new ArithmeticException("Cannot divide by zero quantity.");

            return dividend / divisor;
        }

        /// <summary>
        /// Validates that a quantity has finite numeric value and a resolvable unit.
        /// </summary>
        /// <param name="quantity">The quantity to validate.</param>
        /// <param name="parameterName">The related parameter name for exception metadata.</param>
        /// <exception cref="ArgumentException">Thrown when the quantity is not valid for arithmetic.</exception>
        private static void ValidateFiniteQuantity(Quantity<U> quantity, string parameterName)
        {
            if (!double.IsFinite(quantity.Value))
                throw new ArgumentException("Quantity value must be finite.", parameterName);

            if (!MeasurableResolver.TryResolve(quantity.Unit, out _))
                throw new ArgumentException($"Unsupported unit: {quantity.Unit}", parameterName);
        }

        /// <summary>
        /// Gets the runtime unit enum type used to enforce cross-category comparison safety.
        /// </summary>
        Type IQuantityComparable.UnitType => Unit.GetType();

        /// <summary>
        /// Gets the base-unit value used for comparison and arithmetic.
        /// </summary>
        double IQuantityComparable.BaseValue => BaseValue;

        /// <summary>
        /// Gets the value converted to the category base unit.
        /// </summary>
        private double BaseValue
        {
            get { return ResolveMeasurable(Unit).ConvertToBaseUnit(Value); }
        }

        /// <summary>
        /// Returns a display string in Quantity(value, unit) format.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}
