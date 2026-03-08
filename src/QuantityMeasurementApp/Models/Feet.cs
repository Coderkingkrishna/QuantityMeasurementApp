using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The Feet class represents a measurement in feet and provides methods for equality comparison. if two feet values are equal then it will return true otherwise false.
    /// it also provides a method to get the hash code of the feet value.
    /// It implements the IEquatable<Feet> interface to provide type-specific equality comparison.
    /// </summary>
    /// <remarks>
    /// Legacy UC1 value object retained for backward compatibility alongside the generic UC10 model.
    /// </remarks>
    public sealed class Feet : IEquatable<Feet>
    {
        //to store the value of feet
        private readonly double _value;

        //to get the value of feet
        public double Value => _value;

        //constructor for feet class to initlize the feet value
        public Feet(double value)
        {
            _value = value;
        }

        //to compare two feet values for equality
        public bool Equals(Feet? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _value.CompareTo(other._value) == 0;
        }

        //to compare feet value with another object
        public override bool Equals(object? obj)
        {
            if (obj is not Feet other)
                return false;

            return Equals(other);
        }

        //to get feet value hash code
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
