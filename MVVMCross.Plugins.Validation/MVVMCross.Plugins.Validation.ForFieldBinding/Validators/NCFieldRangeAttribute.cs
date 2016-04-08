using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCross.Plugins.Validation
{
    public class NCFieldRangeAttribute : ValidationAttribute
    {
        private NCFieldRangeAttribute(string message = null) 
            : base(message)
        {
        }

        private NCFieldRangeAttribute(string minimum, string maximum, string message = null)
            : base(message)
        {
            Maximum = maximum;
            Minimum = minimum;
        }

        public NCFieldRangeAttribute(double minimum, double maximum, string message = null) 
            : this(minimum.ToString(), maximum.ToString(), message)
        {
        }

        public NCFieldRangeAttribute(int minimum, int maximum, string message = null) 
            : this(minimum.ToString(), maximum.ToString(), message)
        {
        }

        public object Maximum { get; private set; }

        public object Minimum { get; private set; }

        public override IValidation CreateValidation(Type valueType)
        {
            if (valueType.Name != "INC`1" || valueType.GenericTypeArguments.Length != 1)
                throw new NotSupportedException("RangeAttribute Validator for type " + valueType.Name + " is not supported.");

            var allowTypes = new List<Type>
            {
                typeof(short),
                typeof(int),
                typeof(float),
                typeof(double),
                typeof(ushort),
                typeof(uint),
            };

            var genericityType = valueType.GenericTypeArguments[0];
            if (!allowTypes.Contains(genericityType))
                throw new NotSupportedException("RangeAttribute Validator for type INC<" + genericityType.Name + "> is not supported.");

            return new NCFieldRangeValidation(num => num.Value >= decimal.Parse(Minimum.ToString()) && num.Value <= decimal.Parse(Maximum.ToString()), 
                Minimum, Maximum, Message);
        }
    }
}
