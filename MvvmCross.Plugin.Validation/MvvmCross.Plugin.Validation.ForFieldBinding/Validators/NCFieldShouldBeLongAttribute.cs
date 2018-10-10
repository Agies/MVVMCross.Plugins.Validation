using System;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
{
    public class NCFieldShouldBeLongAttribute : NCFieldValidationAttribute
    {
        public NCFieldShouldBeLongAttribute(string message = null)
            : base(message)
        {
        }

        public override IValidation CreateValidation(Type valueType)
        {
            return new NCFieldShouldBeLongValidation(Message);
        }
    }
}