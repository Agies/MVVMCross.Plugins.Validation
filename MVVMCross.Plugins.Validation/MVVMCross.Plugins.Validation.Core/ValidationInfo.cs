using System.Reflection;

namespace MVVMCross.Plugins.Validation.Core
{
    public class ValidationInfo : IValidationInfo
    {
        public ValidationInfo(PropertyInfo property, IValidation validation, string[] groups)
        {
            Groups = groups;
            Property = property;
            Validation = validation;
        }

        public PropertyInfo Property { get; private set; }
        public IValidation Validation { get; private set; }
        public string[] Groups { get; private set; }

        public override string ToString()
        {
            return string.Format("Validating {0} with {1} during {2}", Property.Name, Validation, string.Join(", ", (Groups ?? new[]{"All"})));
        }
    }
}