﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace MvvmCross.Plugins.Validation.ViewModels
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