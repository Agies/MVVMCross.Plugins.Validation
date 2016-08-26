using System;

namespace MvvmCross.Plugins.Validation.ForFieldBinding
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public abstract class NCFieldValidationAttribute : ValidationAttribute
    {
        protected NCFieldValidationAttribute(string message) : base(message)
        {
            
        }
    }
}