using System;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// The Inches class represents a measurement in inches and provides methods for equality comparison. if two inches values are equal then it will return true otherwise false.
    /// it also provides a method to get the hash code of the inches value.
    /// It implements the IEquatable<Inches> interface to provide type-specific equality comparison.
    /// </summary>
    public sealed class Inches : IEquatable<Inches>
    {
        //to store the value of inches
        private readonly double _value;

        //to get the value of inches
        public double Value => _value;

        //constructor to initlize the inches value
        public Inches(double value)
        {
            _value = value;
        }

        //to compare two inches values for equality
        public bool Equals(Inches? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _value.CompareTo(other._value) == 0;
        }

        //to compare inches value with another object
        public override bool Equals(object? obj)
        {
            if (obj is not Inches other)
                return false;

            return Equals(other);
        }

        //to get inches value hash code
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
