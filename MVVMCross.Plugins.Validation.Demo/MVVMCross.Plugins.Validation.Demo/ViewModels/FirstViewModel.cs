using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Platform;
using MvvmCross.Plugins.Validation.ForFieldBinding;

namespace MvvmCross.Plugins.Validation.Demo.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {

        /// <summary>
        /// Use MvvmCross.Plugin.FieldBinding
        /// </summary>
        [NCFieldRequired("{0} is required")]
        public INC<string> Name = new NC<string>();


        private int age;

        [Range(18, 60, "{0} must between {1} and {2}")]
        public int Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }

        public IMvxCommand SubmitCommand { get; set; }


        IMvxToastService toastService;
        IValidator validator;

        public FirstViewModel(IValidator validator, IMvxToastService toastService)
        {
            this.toastService = toastService;
            this.validator = validator;
        }

        public override void Start()
        {
            SubmitCommand = new MvxCommand(OnSubmit);
        }

        private void OnSubmit()
        {
            var errors = validator.Validate(this);
            if (!errors.IsValid)
            {
                toastService.DisplayErrors(errors); //Display errors here.
                return;
            }
            toastService.DisplayMessage("Submitted");
        }
    }
}
