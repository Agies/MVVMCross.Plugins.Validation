using MvvmCross.Plugin.Messenger;

namespace MvvmCross.Plugin.Validation
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