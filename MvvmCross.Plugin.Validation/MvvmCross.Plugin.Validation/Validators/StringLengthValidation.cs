using System;

namespace MvvmCross.Plugin.Validation
{
    public class StringLengthValidation : IValidation
    {
        private readonly Func<string, bool> _predicate;
        private string _message;
        private int _maximumLength;
        private int _minimumLength;

        public StringLengthValidation(Func<string, bool> predicate, int minimumLength, int maximumLength, string message)
        {
            _predicate = predicate;
            _maximumLength = maximumLength;
            _minimumLength = minimumLength;
            _message = message;
        }

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            if (value == null)
                return null;
            return Validate(propertyName, value.ToString(), subject);
        }

        public IErrorInfo Validate(string propertyName, string value, object subject)
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