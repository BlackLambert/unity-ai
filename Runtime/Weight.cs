using System;
using System.Globalization;

namespace SBaier.AI
{
    public struct Weight : IComparable<Weight>
    {
        public float Value { get; }

        public Weight(float value)
        {
            Value = value;
        }

        public int CompareTo(Weight other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Weight other))
                return false;

            return CompareFloat(other);
        }

        public bool Equals(Weight other)
        {
            return CompareFloat(other);
        }

        private bool CompareFloat(Weight other)
        {
            return Math.Abs(this.Value - other.Value) < float.Epsilon;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}