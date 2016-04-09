using System.Reflection;

namespace MVVMCross.Plugins.Validation
{
    public interface IValidationInfo
    {
        MemberInfo Member { get; }
        IValidation Validation { get; }
        string[] Groups { get; }
    }
}