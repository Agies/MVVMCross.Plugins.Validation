using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Widget;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.Bindings.SourceSteps;
using Cirrious.MvvmCross.Binding.Droid.Target;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Plugins.Messenger;
using MVVMCross.Plugins.Validation.Core;

namespace MVVMCross.Plugins.Validation.Droid
{
    public interface IMvxValidationService
    {
        void SetupForValidation(MvxFragment fragment);
    }

    public class MvxValidationService : IMvxValidationService
    {
        private static readonly PropertyInfo TextViewGetter;
        private static readonly FieldInfo TargetBindingGetter;
        private static readonly FieldInfo BindingDescriptionGetter;

        private Dictionary<string, List<TextView>> _sourceBindingRelationships;
        private MvxFragment _fragment;
        private readonly IValidator _validator;
        private readonly IMvxMessenger _messenger;

        static MvxValidationService()
        {
            Type fullBindingtype = typeof(MvxFullBinding);
            Type textBinding = typeof(MvxTextViewTextTargetBinding);
            BindingDescriptionGetter = fullBindingtype.GetField("_bindingDescription",
                                                                BindingFlags.NonPublic | BindingFlags.GetField |
                                                                BindingFlags.Instance);
            TargetBindingGetter = fullBindingtype.GetField("_targetBinding",
                                                           BindingFlags.NonPublic | BindingFlags.GetField |
                                                           BindingFlags.Instance);
            TextViewGetter = textBinding.GetProperty("TextView",
                                                     BindingFlags.NonPublic | BindingFlags.GetProperty |
                                                     BindingFlags.Instance);
        }

        public MvxValidationService(IValidator validator, IMvxMessenger messenger)
        {
            _validator = validator;
            _messenger = messenger;
            _messenger.Subscribe<ValidationMessage>(OnValidationReceived);
        }

        public void SetupForValidation(MvxFragment fragment)
        {
            _sourceBindingRelationships = new Dictionary<string, List<TextView>>();
            _fragment = fragment;

            var type = _fragment.BindingContext.GetType().BaseType;
            var property = type.GetField("_viewBindings",
                                         BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

            if (property != null)
            {
                var value = (List<KeyValuePair<object, IList<IMvxUpdateableBinding>>>)property.GetValue(_fragment.BindingContext);
                foreach (var keyValuePair in value)
                {
                    RegisterBindings(keyValuePair.Value);
                }
            }
            else
            {
                MvxTrace.Error("_viewBindings on {0} cannot be found.", type);
            }
        }

        private void OnValidationReceived(ValidationMessage message)
        {
            if (_fragment == null || _sourceBindingRelationships == null) return;

            if (_fragment.ViewModel == message.Sender)
            {
                foreach (var sourceBindingRelationship in _sourceBindingRelationships)
                {
                    foreach (var textView in sourceBindingRelationship.Value)
                    {
                        textView.Error = null;
                    }
                }
                if (!message.Validation.IsValid)
                    _fragment.View.ScrollTo(0, 0);
                TextView firstText = null;
                foreach (IErrorInfo errorInfo in message.Validation)
                {
                    List<TextView> texts;
                    if (_sourceBindingRelationships.TryGetValue(errorInfo.PropertyName, out texts))
                    {
                        foreach (TextView editText in texts)
                        {
                            if (firstText == null)
                                firstText = editText;
                            editText.Error = errorInfo.Message;
                        }
                    }
                }
                if (firstText != null)
                    firstText.RequestFocus();
            }
        }

        private void RegisterBindings(IEnumerable<IMvxUpdateableBinding> expected)
        {
            foreach (var binding in expected.OfType<MvxFullBinding>())
            {
                string sourcePath = null;
                TextView boundView = null;

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
                    var targetBinding = TargetBindingGetter.GetValue(binding) as MvxTextViewTextTargetBinding;
                    if (targetBinding != null)
                    {
                        if (TextViewGetter != null)
                        {
                            boundView = (TextView)TextViewGetter.GetValue(targetBinding);
                        }
                        else
                        {
                            MvxTrace.Error("TextView on {0} cannot be found.", typeof(MvxTextViewTextTargetBinding));
                        }
                    }
                }
                else
                {
                    MvxTrace.Error("_targetBinding on {0} cannot be found.", typeof(MvxFullBinding));
                }

                if (sourcePath != null && boundView != null)
                {
                    List<TextView> collection;
                    if (!_sourceBindingRelationships.TryGetValue(sourcePath, out collection))
                    {
                        collection = new List<TextView>();
                        _sourceBindingRelationships[sourcePath] = collection;
                    }
                    if (_validator.IsRequired(_fragment.ViewModel, sourcePath))
                    {
                        if (boundView.Hint.IsNotNullOrEmpty() && !boundView.Hint.EndsWith("*"))
                            boundView.Hint += "*";
                    }
                    collection.Add(boundView);
                }
            }
        }

    }
}