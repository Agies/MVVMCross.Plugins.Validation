using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.MvvmCross.Binding.Touch.Target;
using Cirrious.MvvmCross.Plugins.Messenger;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MVVMCross.Plugins.Validation.Core;

namespace MVVMCross.Plugins.Validation.Touch
{
    public class MvxTouchValidationService : MvxValidationService
    {
        private Dictionary<string, List<UIView>> _sourceBindingRelationships;
        private UIView _firstText;
        private float? _defaultWidth;
        private CGColor _defaultColor;

        IMvxToastService toastService;

        private static readonly PropertyInfo UITextViewGetter;
        private static readonly PropertyInfo UITextFieldGetter;

        static MvxTouchValidationService()
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

        public MvxTouchValidationService(IMvxToastService toastService, IValidator validator, IMvxMessenger messenger)
            : base(validator, messenger)
        {
            this.toastService = toastService;
        }

        public override void SetupForValidation(Cirrious.MvvmCross.Binding.BindingContext.IMvxBindingContext context, Cirrious.MvvmCross.ViewModels.IMvxViewModel viewModel)
        {
            _sourceBindingRelationships = new Dictionary<string, List<UIView>>();
            base.SetupForValidation(context, viewModel);
        }

        protected override void Validated(IErrorCollection errors)
        {
            if (_firstText != null)
                _firstText.BecomeFirstResponder();
            toastService.DisplayErrors(errors);
        }

        protected override void Validate(IErrorInfo errorInfo)
        {
            List<UIView> texts;
            if (_sourceBindingRelationships.TryGetValue(errorInfo.PropertyName, out texts))
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
                if (firstRelationship.Value != null)
                {
                    var firstView = firstRelationship.Value.FirstOrDefault();
                    if (firstView != null)
                    {
                        _defaultColor = firstView.Layer.BorderColor;
                        _defaultWidth = firstView.Layer.BorderWidth;
                    }
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

            List<UIView> collection;
            if (!_sourceBindingRelationships.TryGetValue(sourcePath, out collection))
            {
                collection = new List<UIView>();
                _sourceBindingRelationships[sourcePath] = collection;
            }
            collection.Add(boundView);
        }

        protected override object GetTargetFromBinding(object targetBinding)
        {
            object view = null;
            var textViewBinding = targetBinding as MvxUITextViewTextTargetBinding;
            if (textViewBinding != null)
            {
                var viewView = UITextViewGetter.GetValue(textViewBinding) as UITextView;
                if (viewView != null)
                {

                }
                view = viewView;
            }

            var textFieldBinding = targetBinding as MvxUITextFieldTextTargetBinding;
            if (textFieldBinding != null)
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