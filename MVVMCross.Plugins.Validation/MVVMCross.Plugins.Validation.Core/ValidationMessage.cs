using Cirrious.MvvmCross.Plugins.Messenger;

namespace MVVMCross.Plugins.Validation
{
    public class ValidationMessage : MvxMessage
    {
        public IErrorCollection Validation { get; private set; }

        public ValidationMessage(IErrorCollection validation, object sender)
            : base(sender)
        {
            Validation = validation;
        }
    }
}