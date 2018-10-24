namespace MvvmCross.Plugin.Validation.Android
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterType<IMvxValidationService, MvxAndroidValidationService>();
        }
    }
}