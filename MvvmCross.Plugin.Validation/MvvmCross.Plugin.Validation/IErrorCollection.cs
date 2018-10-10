using System.Collections.Generic;

namespace MvvmCross.Plugin.Validation
{
    public interface IErrorCollection : ICollection<IErrorInfo>
    {
        bool IsValid { get; }
        string Format();
    }
}