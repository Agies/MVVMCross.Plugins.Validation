using System;

namespace MvvmCross.Plugin.Validation.ForFieldBinding.Validators
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