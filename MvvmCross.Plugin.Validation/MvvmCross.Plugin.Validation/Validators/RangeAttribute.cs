using System;
using System.Collections.Generic;

namespace MvvmCross.Plugin.Validation
{
    public class RangeAttribute : ValidationAttribute
    {
        private RangeAttribute(string message = null) 
            : base(message)
        {
        }

        private RangeAttribute(Type type, string minimum, string maximum, string message = null)
            : base(message)
        {
            Maximum = maximum;
            Minimum = minimum;
        }

        public RangeAttribute(double minimum, double maximum, string message = null) 
            : this(typeof(double), minimum.ToString(), maximum.ToString(), message)
        {
        }

        public RangeAttribute(int minimum, int maximum, string message = null) 
            : this(typeof(int), minimum.ToString(), maximum.ToString(), message)
        {
        }

        public object Maximum { get; private set; }

        public object Minimum { get; private set; }

        public Type OperandType { get; private set; }

        public override IValidation CreateValidation(Type valueType)
        {
            var allowTypes = new List<Type>
            {
                typeof(short),
                typeof(int),
                typeof(float),
                typeof(double),
                typeof(ushort),
                typeof(uint),
            };

            if (!allowTypes.Contains(valueType))
                throw new NotSupportedException("RangeAttribute Validator for type " + valueType.Name + " is not supported.");

            return new RangeValidation(num => num >= decimal.Parse(Minimum.ToString()) && num <= decimal.Parse(Maximum.ToString()), 
                Minimum, Maximum, Message);
        }
    }
}
