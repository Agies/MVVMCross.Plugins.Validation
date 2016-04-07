using System;

namespace MVVMCross.Plugins.Validation
{
    public class StringLengthValidation<T> : IValidation<T>
    {
        private readonly Func<T, bool> _predicate;
        private string _message;
        private int _maximumLength;
        private int _minimumLength;

        public StringLengthValidation(Func<T, bool> predicate, int minimumLength, int maximumLength, string message)
        {
            _predicate = predicate;
            _maximumLength = maximumLength;
            _minimumLength = minimumLength;
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
                return new ErrorInfo(propertyName, _message == null ? 
                    string.Format("The Length of {0} must between {1} and {2}", propertyName, _minimumLength, _maximumLength) : 
                    string.Format(_message, propertyName, _minimumLength, _maximumLength)
                    );
            }
            return null;
        }
    }
}