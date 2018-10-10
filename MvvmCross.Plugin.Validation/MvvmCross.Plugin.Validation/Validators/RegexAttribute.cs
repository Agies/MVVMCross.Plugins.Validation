using System;

namespace MvvmCross.Plugin.Validation
{
    public class RegexAttribute : ValidationAttribute
    {
        private readonly string _regex;

        public RegexAttribute(string regex, string message)
            : base(message)
        {
            _regex = regex;
        }

        public override IValidation CreateValidation(Type valueType)
        {
            return new RegexValidation(_regex, Message);
        }
    }
}