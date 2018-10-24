using System;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
{
    public class NCFieldCollectionCountAttribute : NCFieldValidationAttribute
    {
        private NCFieldCollectionCountAttribute(string message = null) 
            : base(message)
        {
        }

        private NCFieldCollectionCountAttribute(string minimum, string maximum, string message = null)
            : base(message)
        {
            Maximum = maximum;
            Minimum = minimum;
        }

        public NCFieldCollectionCountAttribute(int minimum, int maximum, string message = null) 
            : this(minimum.ToString(), maximum.ToString(), message)
        {
        }

        public object Maximum { get; private set; }

        public object Minimum { get; private set; }

        public override IValidation CreateValidation(Type valueType)
        {
            if (!valueType.FullName.Contains("MvvmCross.Plugin.FieldBinding"))
                throw new NotSupportedException("NCFieldCollectionCountAtribute Validator for type " + valueType.Name + " is not supported.");

            return new NCFieldCollectionCountValidation(num => num >= int.Parse(Minimum.ToString()) && num <= int.Parse(Maximum.ToString()),
                Minimum, Maximum, Message);
        }
    }
}
