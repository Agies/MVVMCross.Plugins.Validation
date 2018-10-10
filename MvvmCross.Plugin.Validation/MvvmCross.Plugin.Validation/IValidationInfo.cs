using System.Reflection;

namespace MvvmCross.Plugin.Validation
{
    public interface IValidationInfo
    {
        MemberInfo Member { get; }
        IValidation Validation { get; }
        string[] Groups { get; }
    }
}