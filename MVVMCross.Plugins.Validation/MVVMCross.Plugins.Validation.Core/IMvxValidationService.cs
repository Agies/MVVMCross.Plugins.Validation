using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MVVMCross.Plugins.Validation
{
    public interface IMvxValidationService
    {
        void SetupForValidation(IMvxBindingContext context, IMvxViewModel viewModel);
    }

    public abstract class MvxValidationService : IMvxValidationService
    {
        protected static FieldInfo TargetBindingGetter;
        protected static FieldInfo PropertyGetter;
        protected static FieldInfo BindingDescriptionGetter;
        protected static FieldInfo DirectGetter;

        private readonly IMvxMessenger _messenger;
        private readonly IValidator _validator;

        static MvxValidationService()
        {
            Initialize();
        }

        protected MvxValidationService(IValidator validator, IMvxMessenger messenger)
        {
            _validator = validator;
            _messenger = messenger;
            _messenger.Subscribe<ValidationMessage>(OnValidationReceived, MvxReference.Strong);
        }

        public IMvxMessenger Messenger
        {
            get { return _messenger; }
        }

        protected IMvxBindingContext Context { get; private set; }

        protected IMvxViewModel ViewModel { get; private set; }

        protected IValidator Validator
        {
            get { return _validator; }
        }

        public virtual void SetupForValidation(IMvxBindingContext context, IMvxViewModel viewModel)
        {
            Context = context;
            ViewModel = viewModel;

            if (PropertyGetter != null)
            {
                var value = (List<KeyValuePair<object, IList<IMvxUpdateableBinding>>>)PropertyGetter.GetValue(Context);
                var bindings = new List<IMvxUpdateableBinding>(value.SelectMany(t => t.Value));
                bindings.AddRange((IList<IMvxUpdateableBinding>)DirectGetter.GetValue(Context));
                RegisterBindings(bindings);
            }
            else
            {
                MvxTrace.Error("_viewBindings on {0} cannot be found.", typeof(MvxBindingContext));
            }
        }

        protected static void Initialize()
        {
            var fullBindingtype = typeof(MvxFullBinding).GetTypeInfo();
            var type = typeof(MvxBindingContext).GetTypeInfo();

            PropertyGetter = type.GetDeclaredField("_viewBindings");
            DirectGetter = type.GetDeclaredField("_directBindings");
            BindingDescriptionGetter = fullBindingtype.GetDeclaredField("_bindingDescription");
            TargetBindingGetter = fullBindingtype.GetDeclaredField("_targetBinding");
        }

        protected virtual void OnValidationReceived(ValidationMessage message)
        {
            if (Context == null) return;

            if (ViewModel != message.Sender) return;

            if (!Validating()) return;

            foreach (IErrorInfo errorInfo in message.Validation)
            {
                Validate(errorInfo);
            }

            Validated(message.Validation);
        }

        protected abstract void Validated(IErrorCollection errors);

        protected abstract void Validate(IErrorInfo errorInfo);

        protected abstract bool Validating();

        protected virtual void RegisterBindings(IEnumerable<IMvxUpdateableBinding> expected)
        {
            foreach (MvxFullBinding binding in expected.OfType<MvxFullBinding>())
            {
                string sourcePath = null;
                object boundView = null;

                if (BindingDescriptionGetter != null)
                {
                    var bindingDescription = BindingDescriptionGetter.GetValue(binding) as MvxBindingDescription;
                    if (bindingDescription != null)
                    {
                        var source = bindingDescription.Source as MvxPathSourceStepDescription;
                        if (source != null)
                        {
                            sourcePath = source.SourcePropertyPath;
                        }
                    }
                }
                else
                {
                    MvxTrace.Error("_bindingDescription on {0} cannot be found.", typeof(MvxFullBinding));
                }

                if (TargetBindingGetter != null)
                {
                    var targetBinding = TargetBindingGetter.GetValue(binding);
                    if (targetBinding != null)
                    {
                        boundView = GetTargetFromBinding(targetBinding);
                    }
                }
                else
                {
                    MvxTrace.Error("_targetBinding on {0} cannot be found.", typeof(MvxFullBinding));
                }

                if (sourcePath != null && boundView != null)
                {
                    ProcessSourceAndTarget(sourcePath, boundView);
                }
            }
        }

        protected abstract void ProcessSourceAndTarget(string sourcePath, object target);

        protected abstract object GetTargetFromBinding(object targetBinding);
    }
}