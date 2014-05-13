using System.Text.RegularExpressions;

namespace MVVMCross.Plugins.Validation.Core
{
    public class RegexValidation : IValidation
    {
        private readonly string _message;
        private readonly Regex _regex;

        public RegexValidation(string regex, string message)
        {
            _message = message;
            _regex = new Regex(regex);
        }

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            if (value == null) return null;

            var stringValue = value.ToString();
            if (!_regex.IsMatch(stringValue))
                return new ErrorInfo(propertyName, string.Format(_message, propertyName));
            return null;
        }
    }
}