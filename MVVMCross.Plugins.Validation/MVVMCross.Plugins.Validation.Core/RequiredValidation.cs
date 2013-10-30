using System;

namespace MVVMCross.Plugins.Validation.Core
{
    public class RequiredValidation<T> : ValidationBase<T>
    {
        private readonly Func<T, bool> _predicate;

        public RequiredValidation(Func<T, bool> predicate, string message) : base(message ?? "is Required")
        {
            _predicate = predicate;
        }

        public override IErrorInfo Validate(string propertyName, T value, object subject)
        {
            if (!_predicate(value))
            {
                return new ErrorInfo(propertyName, FormatMessage(propertyName));
            }
            return null;
        }
    }
}