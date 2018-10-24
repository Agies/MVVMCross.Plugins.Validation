using System;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
{
    public class NCFieldRangeAttribute : NCFieldValidationAttribute
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
            if (!valueType.FullName.Contains("MvvmCross.Plugin.FieldBinding"))
                throw new NotSupportedException("NCFieldRange Validator for type " + valueType.Name + " is not supported.");

            return new NCFieldRangeValidation(num => num.Value >= decimal.Parse(Minimum.ToString()) && num.Value <= decimal.Parse(Maximum.ToString()), 
                Minimum, Maximum, Message);
        }
    }
}
