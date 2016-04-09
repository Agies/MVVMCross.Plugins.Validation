using MvvmCross.FieldBinding;
using System;

namespace MVVMCross.Plugins.Validation.ForFieldBinding
{
    public class NCFieldStringLengthValidation : IValidation
    {
        private readonly Func<INC<string>, bool> _predicate;
        private string _message;
        private int _maximumLength;
        private int _minimumLength;

        public NCFieldStringLengthValidation(Func<INC<string>, bool> predicate, int minimumLength, int maximumLength, string message)
        {
            _predicate = predicate;
            _maximumLength = maximumLength;
            _minimumLength = minimumLength;
            _message = message;
        }

        public IErrorInfo Validate(string fieldName, object value, object subject)
        {
            return Validate(fieldName, value as INC<string>, subject);
        }

        public IErrorInfo Validate(string fieldName, INC<string> value, object subject)
        {
            if (!_predicate(value))
            {
                return new ErrorInfo(fieldName, _message == null ? 
                    string.Format("The Length of {0} must between {1} and {2}", fieldName, _minimumLength, _maximumLength) : 
                    string.Format(_message, fieldName, _minimumLength, _maximumLength)
                    );
            }
            return null;
        }
    }
}