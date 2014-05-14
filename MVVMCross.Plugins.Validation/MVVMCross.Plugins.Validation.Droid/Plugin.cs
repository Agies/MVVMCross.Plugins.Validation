using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using MVVMCross.Plugins.Validation;

namespace MVVMCross.Plugins.Validation.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterType<IMvxValidationService, MvxAndroidValidationService>();
        }
    }
}