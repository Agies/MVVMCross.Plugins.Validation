using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Logging;
using MvvmCross.Platforms.Ios.Binding.Target;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using UIKit;

namespace MvvmCross.Plugin.Validation.Ios
{
    public class MvxIosValidationService : MvxValidationService
    {
        private Dictionary<string, List<UIView>> _sourceBindingRelationships;
        private UIView _firstText;
        private nfloat? _defaultWidth;
        private CGColor _defaultColor;

        private readonly IMvxToastService _toastService;

        private static readonly PropertyInfo UITextViewGetter;
        private static readonly PropertyInfo UITextFieldGetter;

        static MvxIosValidationService()
        {
            var bindingType = typeof(MvxUITextViewTextTargetBinding);
            UITextViewGetter = bindingType.GetProperty("View",
                                                           BindingFlags.NonPublic | BindingFlags.GetProperty |
                                                           BindingFlags.Instance);
            bindingType = typeof(MvxUITextFieldTextTargetBinding);
            UITextFieldGetter = bindingType.GetProperty("View",
                                                           BindingFlags.NonPublic | BindingFlags.GetProperty |
                                                           BindingFlags.Instance);
        }

        public MvxIosValidationService(IMvxToastService toastService, IValidator validator, IMvxMessenger messenger, IMvxLog log)
            : base(validator, messenger, log)
        {
            _toastService = toastService;
        }

        public override void SetupForValidation(IMvxBindingContext context, IMvxViewModel viewModel)
        {
            _sourceBindingRelationships = new Dictionary<string, List<UIView>>();
            base.SetupForValidation(context, viewModel);
        }

        protected override void Validated(IErrorCollection errors)
        {
            _firstText?.BecomeFirstResponder();
            _toastService.DisplayErrors(errors);
        }

        protected override void Validate(IErrorInfo errorInfo)
        {
            if (_sourceBindingRelationships.TryGetValue(errorInfo.MemberName, out var texts))
            {
                foreach (var editText in texts)
                {
                    if (_firstText == null)
                        _firstText = editText;
                    editText.Layer.BorderColor = UIColor.Red.CGColor;
                    editText.Layer.BorderWidth = 3.0f;
                }
            }
        }

        protected override bool Validating()
        {
            if (_defaultColor == null)
            {
                var firstRelationship = _sourceBindingRelationships.FirstOrDefault();
                var firstView = firstRelationship.Value?.FirstOrDefault();
                if (firstView != null)
                {
                    _defaultColor = firstView.Layer.BorderColor;
                    _defaultWidth = firstView.Layer.BorderWidth;
                }
            }

            if (_sourceBindingRelationships == null) return false;
            foreach (var textView in _sourceBindingRelationships.SelectMany(sourceBindingRelationship => sourceBindingRelationship.Value))
            {
                textView.Layer.BorderColor = _defaultColor;
                textView.Layer.BorderWidth = _defaultWidth.GetValueOrDefault();
            }
            return true;
        }

        protected override void ProcessSourceAndTarget(string sourcePath, object target)
        {
            var boundView = target as UIView;
            if (boundView == null) return;

            if (!_sourceBindingRelationships.TryGetValue(sourcePath, out var collection))
            {
                collection = new List<UIView>();
                _sourceBindingRelationships[sourcePath] = collection;
            }
            collection.Add(boundView);
        }

        protected override object GetTargetFromBinding(object targetBinding)
        {
            object view = null;
            if (targetBinding is MvxUITextViewTextTargetBinding textViewBinding)
            {
                var viewView = UITextViewGetter.GetValue(textViewBinding) as UITextView;
                if (viewView != null)
                {

                }
                view = viewView;
            }

            if (targetBinding is MvxUITextFieldTextTargetBinding textFieldBinding)
            {
                var textField = UITextFieldGetter.GetValue(textFieldBinding) as UITextField;
                if (textField != null)
                {
                    if (textField.Placeholder.IsNotNullOrEmpty() && !textField.Placeholder.EndsWith("*"))
                    {
                        textField.Placeholder += "*";
                    }
                }
                view = textField;
            }
            return view;
        }
    }
}