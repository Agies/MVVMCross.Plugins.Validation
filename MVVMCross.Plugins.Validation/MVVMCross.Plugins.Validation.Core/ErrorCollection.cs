using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmCross.Plugins.Validation
{
    public class ErrorCollection : Collection<IErrorInfo>, IErrorCollection
    {
        public ErrorCollection(IList<IErrorInfo> result) : base(result)
        {
            
        }

        public bool IsValid { get { return !this.Any(); } }

        public string Format()
        {
            return string.Join("\r\n", this.Select(t => t.Message));
        }
    }
}