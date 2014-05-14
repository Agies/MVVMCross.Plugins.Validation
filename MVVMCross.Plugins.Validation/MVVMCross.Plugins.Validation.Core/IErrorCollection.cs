using System.Collections.Generic;

namespace MVVMCross.Plugins.Validation
{
    public interface IErrorCollection : ICollection<IErrorInfo>
    {
        bool IsValid { get; }
        string Format();
    }
}