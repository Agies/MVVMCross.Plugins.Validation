using System.Text.RegularExpressions;

namespace MvvmCross.Plugin.Validation
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
            if (value == null)
                return null;

            var stringValue = value.ToString();
            if (!_regex.IsMatch(stringValue))
                return new ErrorInfo(propertyName, _message == null ? 
                    string.Format("The value of {0} is incorrect", propertyName) :
                    string.Format(_message, propertyName)
                    );
            return null;
        }
    }
}