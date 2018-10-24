using System.Collections.Generic;
using UIKit;

namespace MvvmCross.Plugin.Validation.Ios
{
    public class MvxIosToastService : IMvxToastService
    {
		void DisplayAlert(string title, string text)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(8,0))
			{
				var view = UIAlertController.Create(title, text, UIAlertControllerStyle.Alert);
				view.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));

				//TODO improve by passing in a reference to the current IMvxTouchView
				UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(view, true, null);
			}
			else
			{
				//Legacy API
				var view = new UIAlertView(title, text, null, "OK");
				view.Show();
			}
		}

        public void DisplayMessage(string text)
        {
			DisplayAlert("Alert", text);
        }

        public void DisplayErrors(List<KeyValuePair<string, string>> errors)
        {
			DisplayAlert("Error", errors.Collect());
        }

        public void DisplayErrors(IErrorCollection errors)
        {
			DisplayAlert("Error", errors.Format());
        }

        public void DisplayError(string error)
        {
			DisplayAlert("Error", error);
        }
    }
}