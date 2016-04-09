using System;

namespace MVVMCross.Plugins.Validation.ForFieldBinding
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