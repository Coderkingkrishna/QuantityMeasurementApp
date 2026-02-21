using System;

namespace QuantityMeasurementApp.Core.Models
{
    public sealed class Inches : IEquatable<Inches>
    {
        private readonly double _value;

        public double Value => _value;

        public Inches(double value)
        {
            _value = value;
        }

        public bool Equals(Inches? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _value.CompareTo(other._value) == 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Inches other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
