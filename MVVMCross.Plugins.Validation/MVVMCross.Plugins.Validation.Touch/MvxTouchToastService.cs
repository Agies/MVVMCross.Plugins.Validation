using System.Collections.Generic;
using MonoTouch.UIKit;
using MVVMCross.Plugins.Validation.Core;

namespace MVVMCross.Plugins.Validation.Touch
{
    public class MvxTouchToastService : IMvxToastService
    {
        public void DisplayMessage(string text)
        {
            var view = new UIAlertView("Alert", text, null, "OK");
            view.Show();
        }

        public void DisplayErrors(List<KeyValuePair<string, string>> errors)
        {
            var view = new UIAlertView("Error", errors.Collect(), null, "OK");
            view.Show();
        }

        public void DisplayErrors(IErrorCollection errors)
        {
            var view = new UIAlertView("Error", errors.Format(), null, "OK");
            view.Show();
        }

        public void DisplayError(string error)
        {
            var view = new UIAlertView("Error", error, null, "OK");
            view.Show();
        }
    }
}