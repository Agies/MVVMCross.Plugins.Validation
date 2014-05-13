using System.Collections.Generic;

namespace MVVMCross.Plugins.Validation.Core
{
    public interface IErrorCollection : ICollection<IErrorInfo>
    {
        bool IsValid { get; }
        string Format();
    }
}