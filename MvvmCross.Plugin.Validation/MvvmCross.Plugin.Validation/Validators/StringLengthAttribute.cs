using System;

namespace MvvmCross.Plugin.Validation
{
    public class StringLengthAttribute : ValidationAttribute
    {
        private StringLengthAttribute(string message = null) : base(message) { }

        public StringLengthAttribute(int maximumLength, string message = null)
            : base(message)
        {
            MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }

        public int MinimumLength { get; set; }

        public override IValidation CreateValidation(Type valueType)
        {
            if (valueType != typeof(string))
                throw new NotSupportedException("StringLength Validator for type " + valueType.Name + " is not supported.");

            return new StringLengthValidation(str =>
            {
                if (str.IsNullOrEmpty())
                    return true;

                return str.Length >= MinimumLength && str.Length <= MaximumLength;
            }, MinimumLength, MaximumLength, Message);
        }
    }
}
