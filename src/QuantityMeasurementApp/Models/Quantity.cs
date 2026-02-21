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
        /// <summary>
        /// Gets the value of the quantity.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets the unit of the quantity.
        /// </summary>
        public LengthUnit Unit { get; }

        /// <summary>
        /// Initializes a new instance of the QuantityLength class with a value and unit.
        /// </summary>
        /// <param name="value">The numerical value of the quantity.</param>
        /// <param name="unit">The unit of measurement for the quantity.</param>
        public QuantityLength(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Compares two QuantityLength instances for equality by converting them to a common base unit (feet).
        /// </summary>
        /// <param name="other">The other Quantity instance to compare with.</param>
        /// <returns>True if the quantities are equal after conversion to the base unit; otherwise, false.</returns>
        public bool Equals(QuantityLength? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            // Convert both quantities to the base unit (feet) for comparison
            double thisValueInBaseUnit = ConvertToBaseUnit(this.Value, this.Unit);
            double otherValueInBaseUnit = ConvertToBaseUnit(other.Value, other.Unit);

            // Compare the values in the base unit with tolerance for floating-point precision
            // Tolerance set to 1e-9 to handle unit conversions with rounding
            return Math.Abs(thisValueInBaseUnit - otherValueInBaseUnit) < 1e-9;
        }

        /// <summary>
        /// Compares this QuantityLength instance with another object for equality.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the object is a Quantity of the same type and is equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not QuantityLength other)
                return false;

            return Equals(other);
        }

        /// <summary>
        /// Gets the hash code of this Quantity instance.
        /// Returns the base unit value's hash code for consistent hashing.
        /// </summary>
        public override int GetHashCode()
        {
            return ConvertToBaseUnit(Value, Unit).GetHashCode();
        }

        /// <summary>
        /// Converts a value from a given unit to the base unit (feet).
        /// This method supports unit conversion for equality comparison.
        /// </summary>
        private static double ConvertToBaseUnit(double value, LengthUnit unit)
        {
            double conversionFactor = unit.GetConversionFactor();
            return value * conversionFactor;
        }

        /// <summary>
        /// Returns a string representation of this QuantityLength instance.
        /// </summary>
        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}
