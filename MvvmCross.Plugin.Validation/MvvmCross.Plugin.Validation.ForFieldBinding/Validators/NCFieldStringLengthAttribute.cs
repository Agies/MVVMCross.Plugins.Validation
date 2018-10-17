using System;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
{
    public class NCFieldStringLengthAttribute : NCFieldValidationAttribute
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
            if (!valueType.FullName.Contains("MvvmCross.Plugin.FieldBinding"))
                throw new NotSupportedException("NCFieldStringLength Validator for type " + valueType.Name + " is not supported.");

            return new NCFieldStringLengthValidation(str =>
            {
                if (str == null || str.Value.IsNullOrEmpty())
                    return true;

                return str.Value.Length >= MinimumLength && str.Value.Length <= MaximumLength;
            }, MinimumLength, MaximumLength, Message);
        }
    }
}
