using System;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
{
    public class NCFieldRequiredValidation<T> : IValidation<T>
    {
        private readonly Func<T, bool> _predicate;
        private string _message;

        public NCFieldRequiredValidation(Func<T, bool> predicate, string message)
        {
            _predicate = predicate;
            _message = message;
        }

        public IErrorInfo Validate(string fieldName, object value, object subject)
        {
            return Validate(fieldName, (T)value, subject);
        }

        public IErrorInfo Validate(string fieldName, T value, object subject)
        {
            if (!_predicate(value))
            {
                return new ErrorInfo(fieldName, _message == null ? string.Format("{0} is Required", fieldName) : string.Format(_message, fieldName));
            }
            return null;
        }
    }
}