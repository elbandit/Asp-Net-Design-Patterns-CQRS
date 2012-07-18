namespace Agathas.Storefront.Common
{
    public class Money
    {
        protected decimal _value;
        // TODO: If we need to change currency we can do nice and easily here

        public Money() : this (0m)
        {
        }

        public Money(decimal value)
        {
            _value = value;
        }

        public bool is_zero
        {
            get { return _value == 0; }            
        }

        public Money multiple_by(int value)
        {
            return new Money(_value * value);
        }

        public Money add(Money value)
        {
            return new Money(_value + value._value);
        }

        public Money minus(Money discount)
        {
            return new Money(_value - discount._value);
        }

        public static bool operator ==(Money valueObject1, Money valueObject2)
        {
            if ((object)valueObject1 == null)
            {
                return (object)valueObject2 == null;
            }

            return valueObject1._value == valueObject2._value;
        }

        public static bool operator !=(Money valueObject1, Money valueObject2)
        {
            return !(valueObject1._value == valueObject2._value);
        }

        public bool Equals(Money obj)
        {
            if (obj == null) return false;         
            return obj._value == _value;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            
            return ((Money)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("£{0}", _value);
        }

        public bool is_greater_than(Money threshold)
        {
            return this._value > threshold._value;
        }

        public bool is_less_than(Money threshold)
        {
            return this._value < threshold._value;
        }
    }
}
