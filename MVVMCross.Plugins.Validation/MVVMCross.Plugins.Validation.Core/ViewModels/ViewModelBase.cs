using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

namespace MVVMCross.Plugins.Validation.ViewModels
{
    public class ViewModelBase : MvxViewModel
    {
        protected IMvxMessenger Messenger { get; private set; }

        public ViewModelBase(IMvxMessenger messenger)
        {
            Messenger = messenger;
        }
    }
}