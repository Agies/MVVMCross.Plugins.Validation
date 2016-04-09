using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;

namespace MVVMCross.Plugins.Validation
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

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            if (value == null)
                return null;

            var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
            if (incValue == null)
                return null;

            var stringValue = incValue.ToString();
            if (!_regex.IsMatch(stringValue))
                return new ErrorInfo(propertyName, _message == null ? 
                    string.Format("The value of {0} is incorrect", propertyName) :
                    string.Format(_message, propertyName)
                    );
            return null;
        }
    }
}