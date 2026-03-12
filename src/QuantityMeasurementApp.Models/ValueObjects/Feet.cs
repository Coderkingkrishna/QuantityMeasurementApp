using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class Feet : IEquatable<Feet>
    {
        private readonly double _value;
        public double Value => _value;

        public Feet(double value)
        {
            _value = value;
        }

        public bool Equals(Feet? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _value.CompareTo(other._value) == 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Feet other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
