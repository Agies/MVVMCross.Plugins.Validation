using System.Reflection;

namespace MvvmCross.Plugin.Validation
{
    public class ValidationInfo : IValidationInfo
    {
        public ValidationInfo(MemberInfo member, IValidation validation, string[] groups)
        {
            Groups = groups;
            Member = member;
            Validation = validation;
        }

        public MemberInfo Member { get; private set; }
        public IValidation Validation { get; private set; }
        public string[] Groups { get; private set; }

        public override string ToString()
        {
            return string.Format("Validating {0} with {1} during {2}", Member.Name, Validation, string.Join(", ", (Groups ?? new[]{"All"})));
        }
    }
}