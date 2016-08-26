using System;

namespace MvvmCross.Plugins.Validation.ForFieldBinding
{
    public class NCFieldRegexAttribute : NCFieldValidationAttribute
    {
        private readonly string _regex;

        public NCFieldRegexAttribute(string regex, string message)
            : base(message)
        {
            _regex = regex;
        }

        public override IValidation CreateValidation(Type valueType)
        {
            return new NCFieldRegexValidation(_regex, Message);
        }
    }
}