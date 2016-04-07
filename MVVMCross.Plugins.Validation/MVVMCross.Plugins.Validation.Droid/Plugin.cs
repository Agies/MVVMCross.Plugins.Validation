using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

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