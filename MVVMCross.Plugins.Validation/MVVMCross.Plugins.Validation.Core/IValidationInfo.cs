using System.Reflection;

namespace MvvmCross.Plugins.Validation
{
    public interface IValidationInfo
    {
        MemberInfo Member { get; }
        IValidation Validation { get; }
        string[] Groups { get; }
    }
}