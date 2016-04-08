using System;

namespace MVVMCross.Plugins.Validation
{
    public class NCFieldRegexAttribute : ValidationAttribute
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