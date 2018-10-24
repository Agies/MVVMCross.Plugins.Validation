namespace MvvmCross.Plugin.Validation.Ios
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterType<IMvxValidationService, MvxIosValidationService>();
        }
    }
}