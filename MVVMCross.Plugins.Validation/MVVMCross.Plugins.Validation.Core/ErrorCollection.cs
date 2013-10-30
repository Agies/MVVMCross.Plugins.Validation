using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMCross.Plugins.Validation.Core
{
    public class ErrorCollection : Collection<IErrorInfo>, IErrorCollection
    {
        public ErrorCollection(IList<IErrorInfo> result) : base(result)
        {
            
        }

        public bool IsValid { get { return !this.Any(); } }
    }
}