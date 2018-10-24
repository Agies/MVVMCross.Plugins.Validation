using System;
using MvvmCross.Plugin.Messenger;

namespace MvvmCross.Plugin.Validation.ViewModels
{
    public abstract class ValidatingViewModel : ViewModelBase
    {
        public IValidator Validator { get; private set; }

        protected ValidatingViewModel(IValidator validator, IMvxMessenger messenger)
            : base(messenger)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");
            Validator = validator;
        }

        public IErrorCollection Validate(string group = null)
        {
            return Validator.Validate(this, group);
        }

        public bool ValidateAndSendMessage(string group = null)
        {
            var result = Validate(group);
            if (!result.IsValid)
            {
                Messenger.Publish(new ValidationMessage(result, this));
            }
            return result.IsValid;
        }

        public bool IsValid(string group = null)
        {
            return Validate(group).IsValid;
        }
    }
}