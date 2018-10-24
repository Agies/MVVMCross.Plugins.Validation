using System;
using System.Linq;
using System.Reflection;
using MvvmCross.Plugin.FieldBinding;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
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
            if (value == null)
                return null;

            var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
            if (incValue == null)
                return null;

            var incValueType = incValue.GetType();
            if (incValueType != typeof(string))
                throw new NotSupportedException("NCFieldStringLength Validator for type " + value.GetType().FullName + " is not supported.");

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