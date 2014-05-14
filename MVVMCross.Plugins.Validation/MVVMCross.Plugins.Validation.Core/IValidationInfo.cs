using System.Reflection;

namespace MVVMCross.Plugins.Validation
{
    public interface IValidationInfo
    {
        PropertyInfo Property { get; }
        IValidation Validation { get; }
        string[] Groups { get; }
    }
}