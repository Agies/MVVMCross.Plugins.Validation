using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace MVVMCross.Plugins.Validation.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterType<IMvxValidationService, MvxTouchValidationService>();
        }
    }
}