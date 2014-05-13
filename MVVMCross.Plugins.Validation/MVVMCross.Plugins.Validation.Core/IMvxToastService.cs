using System.Collections.Generic;

namespace MVVMCross.Plugins.Validation.Core
{
    public interface IMvxToastService
    {
        void DisplayMessage(string text);
        void DisplayErrors(List<KeyValuePair<string, string>> errors);
        void DisplayErrors(IErrorCollection errors);
        void DisplayError(string error);
    }
}