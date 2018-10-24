using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
{
    public class NCFieldRegexValidation : IValidation
    {
        private readonly string _message;
        private readonly Regex _regex;

        public NCFieldRegexValidation(string regex, string message)
        {
            _message = message;
            _regex = new Regex(regex);
        }

        public IErrorInfo Validate(string fieldName, object value, object subject)
        {
            if (value == null)
                return null;

            var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
            if (incValue == null)
                return null;

            var stringValue = incValue.ToString();
            if (!_regex.IsMatch(stringValue))
                return new ErrorInfo(fieldName, _message == null ? 
                    string.Format("The value of {0} is incorrect", fieldName) :
                    string.Format(_message, fieldName)
                    );
            return null;
        }
    }
}