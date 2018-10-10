using System;

namespace MvvmCross.Plugin.Validation
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class ValidationAttribute : Attribute
    {
        public string Message { get; private set; }
        public string[] Groups { get; set; }

        protected ValidationAttribute(string message)
        {
            Message = message;
        }

        public abstract IValidation CreateValidation(Type valueType);
    }
}