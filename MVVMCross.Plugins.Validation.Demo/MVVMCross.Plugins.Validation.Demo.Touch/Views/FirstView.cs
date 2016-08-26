using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;

namespace MvvmCross.Plugins.Validation.Demo.Touch.Views
{
    public partial class FirstView : MvxViewController
    {
        public FirstView() : base("FirstView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, ViewModels.FirstViewModel>();
            set.Bind(LblName).To(vm => vm.Name);
            set.Bind(TxtName).To(vm => vm.Name);
            set.Bind(LblAge).To(vm => vm.Age).WithConversion("AgeFormat");
            set.Bind(TxtAge).To(vm => vm.Age).WithConversion("AgeFormat");
            set.Bind(btnSubmit).To(vm => vm.SubmitCommand);
            set.Apply();
        }
    }
}
