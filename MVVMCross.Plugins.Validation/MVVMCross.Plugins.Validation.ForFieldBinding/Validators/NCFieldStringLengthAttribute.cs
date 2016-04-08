using MvvmCross.FieldBinding;
using System;

namespace MVVMCross.Plugins.Validation
{
    public class NCFieldStringLengthAttribute : ValidationAttribute
    {
        private NCFieldStringLengthAttribute(string message = null) : base(message) { }

        public NCFieldStringLengthAttribute(int maximumLength, string message = null)
            : base(message)
        {
            MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }

        public int MinimumLength { get; set; }

        public override IValidation CreateValidation(Type valueType)
        {
            if (valueType.Name != "INC`1" || valueType.GenericTypeArguments.Length != 1)
                throw new NotSupportedException("RangeAttribute Validator for type " + valueType.Name + " is not supported.");

            var genericityType = valueType.GenericTypeArguments[0];
            if (genericityType != typeof(string))
                throw new NotSupportedException("NCFieldStringLength Validator for type INC<" + genericityType.Name + "> is not supported.");

            return new NCFieldStringLengthValidation(str =>
            {
                if (str == null || str.Value.IsNullOrEmpty())
                    return true;

                return str.Value.Length >= MinimumLength && str.Value.Length <= MaximumLength;
            }, MinimumLength, MaximumLength, Message);
        }
    }
}
