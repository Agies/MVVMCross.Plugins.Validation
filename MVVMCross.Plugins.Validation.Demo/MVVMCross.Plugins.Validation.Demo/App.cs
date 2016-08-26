using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace MvvmCross.Plugins.Validation.Demo
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterType<IValidator, Validator>();

            RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}
