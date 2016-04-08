using System;

namespace MVVMCross.Plugins.Validation
{
    public class NCFieldShouldBeLongAttribute : ValidationAttribute
    {
        public NCFieldShouldBeLongAttribute(string message = null)
            : base(message)
        {
        }

        public override IValidation CreateValidation(Type valueType)
        {
            return new ShouldBeLongValidation(Message);
        }
    }
}