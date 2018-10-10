using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace MvvmCross.Plugin.Validation.ViewModels
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