using System.Collections.Generic;
using System.Linq;
using MvvmCross.ViewModels;

namespace MvvmCross.Plugin.Validation
{
    public class ErrorCollection : MvxObservableCollection<IErrorInfo>, IErrorCollection
    {
        public ErrorCollection(IEnumerable<IErrorInfo> result) : base(result)
        {
            
        }

        public bool IsValid => !this.Any();

        public string Format()
        {
            return string.Join("\r\n", this.Select(t => t.Message));
        }
    }
}