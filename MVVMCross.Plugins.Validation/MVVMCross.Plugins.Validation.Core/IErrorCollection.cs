using System.Collections.Generic;

namespace MvvmCross.Plugins.Validation
{
    public interface IErrorCollection : ICollection<IErrorInfo>
    {
        bool IsValid { get; }
        string Format();
    }
}