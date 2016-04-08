using System;

namespace MVVMCross.Plugins.Validation
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

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            return Validate(propertyName, (T)value, subject);
        }

        public IErrorInfo Validate(string propertyName, T value, object subject)
        {
            if (!_predicate(value))
            {
                return new ErrorInfo(propertyName, _message == null ? string.Format("{0} is Required", propertyName) : string.Format(_message, propertyName));
            }
            return null;
        }
    }
}