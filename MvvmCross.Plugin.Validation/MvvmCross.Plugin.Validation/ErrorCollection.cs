using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmCross.Plugin.Validation
{
    public class ErrorCollection : Collection<IErrorInfo>, IErrorCollection
    {
        public ErrorCollection(IList<IErrorInfo> result) : base(result)
        {
            
        }

        public bool IsValid => !this.Any();

        public string Format()
        {
            return string.Join("\r\n", this.Select(t => t.Message));
        }
    }
}