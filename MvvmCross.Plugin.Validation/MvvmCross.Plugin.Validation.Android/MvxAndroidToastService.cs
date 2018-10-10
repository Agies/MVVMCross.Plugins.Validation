using System.Collections.Generic;
using Android.Content;
using Android.Widget;

namespace MvvmCross.Plugin.Validation.Android
{
    public class MvxAndroidToastService : IMvxToastService
    {
        private readonly Context _context;

        public MvxAndroidToastService(Context context)
        {
            _context = context;
        }

        public void DisplayMessage(string text)
        {
            Toast.MakeText(_context, text, ToastLength.Short).Show();
        }

        public void DisplayErrors(List<KeyValuePair<string, string>> errors)
        {
            Toast.MakeText(_context, errors.Collect(), ToastLength.Long).Show();
        }

        public void DisplayErrors(IErrorCollection errors)
        {
            var view = Toast.MakeText(_context, errors.Format(), ToastLength.Long);
            view.Show();
        }

        public void DisplayError(string error)
        {
            Toast.MakeText(_context, error, ToastLength.Long).Show();
        }
    }
}