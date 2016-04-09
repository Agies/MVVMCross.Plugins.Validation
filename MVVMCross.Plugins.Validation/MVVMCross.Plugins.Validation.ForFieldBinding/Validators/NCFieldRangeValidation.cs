using MvvmCross.FieldBinding;
using System;
using System.Linq;
using System.Reflection;

namespace MVVMCross.Plugins.Validation.ForFieldBinding
{
    public class NCFieldRangeValidation : IValidation
    {
        private readonly Func<INC<decimal>, bool> _predicate;
        private string _message;
        private object _maximum;
        private object _minimum;

        public NCFieldRangeValidation(Func<INC<decimal>, bool> predicate, object minimum, object maximum, string message)
        {
            _predicate = predicate;
            _minimum = minimum;
            _maximum = maximum;
            _message = message;
        }

        public IErrorInfo Validate(string fieldName, object value, object subject)
        {
            if (value == null)
                return null;

            var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
            return Validate(fieldName, new NC<decimal>(decimal.Parse(incValue.ToString())), subject);
        }

        public IErrorInfo Validate(string fieldName, INC<decimal> value, object subject)
        {
            if (!_predicate(value))
            {
                return new ErrorInfo(fieldName, _message == null ? 
                    string.Format("The Range of {0} must between {1} and {2}", fieldName, _minimum, _maximum) : 
                    string.Format(_message, fieldName, _minimum, _maximum)
                    );
            }
            return null;
        }
    }
}