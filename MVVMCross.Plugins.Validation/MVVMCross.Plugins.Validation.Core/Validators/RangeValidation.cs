using System;

namespace MVVMCross.Plugins.Validation
{
    public class RangeValidation<T> : IValidation<T>
    {
        private readonly Func<T, bool> _predicate;
        private string _message;
        private object _maximum;
        private object _minimum;

        public RangeValidation(Func<T, bool> predicate, object minimum, object maximum, string message)
        {
            _predicate = predicate;
            _minimum = minimum;
            _maximum = maximum;
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
                    string.Format("The Range of {0} must between {1} and {2}", propertyName, _minimum, _maximum) : 
                    string.Format(_message, propertyName, _minimum, _maximum)
                    );
            }
            return null;
        }
    }
}