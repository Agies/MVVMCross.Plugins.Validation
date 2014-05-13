using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Text;
using Android.Widget;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Target;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using MVVMCross.Plugins.Validation.Core;

namespace MVVMCross.Plugins.Validation.Droid
{
    public class MvxAndroidValidationService : MvxValidationService
    {
        private static readonly PropertyInfo TextViewGetter;

        private Dictionary<string, List<TextView>> _sourceBindingRelationships;

        static MvxAndroidValidationService()
        {
            Initialize();

            Type textBinding = typeof(MvxTextViewTextTargetBinding);
            TextViewGetter = textBinding.GetProperty("TextView",
                                                     BindingFlags.NonPublic | BindingFlags.GetProperty |
                                                     BindingFlags.Instance);
        }

        public MvxAndroidValidationService(IValidator validator, IMvxMessenger messenger)
            : base(validator, messenger)
        {
        }

        public override void SetupForValidation(IMvxBindingContext context, IMvxViewModel viewModel)
        {
            _sourceBindingRelationships = new Dictionary<string, List<TextView>>();
            base.SetupForValidation(context, viewModel);
        }

        TextView _firstText;

        protected override void Validate(IErrorInfo errorInfo)
        {
            List<TextView> texts;
            if (_sourceBindingRelationships.TryGetValue(errorInfo.PropertyName, out texts))
            {
                foreach (TextView editText in texts)
                {
                    if (_firstText == null)
                        _firstText = editText;
                    editText.ErrorFormatted = Html.FromHtml(string.Format("<font color='black'>{0}</font>", errorInfo.Message));
                }
            }
        }

        protected override void Validated(IErrorCollection errors)
        {
            if (_firstText != null)
            {
                _firstText.RequestFocus();
                _firstText = null;
            }
        }

        protected override bool Validating()
        {
            if (_sourceBindingRelationships == null) return false;
            foreach (
                var textView in
                    _sourceBindingRelationships.SelectMany(sourceBindingRelationship => sourceBindingRelationship.Value))
            {
                textView.Error = null;
            }
            return true;
        }

        protected override void ProcessSourceAndTarget(string sourcePath, object target)
        {
            List<TextView> collection;
            var boundView = target as TextView;
            if (boundView == null) return;

            if (!_sourceBindingRelationships.TryGetValue(sourcePath, out collection))
            {
                collection = new List<TextView>();
                _sourceBindingRelationships[sourcePath] = collection;
            }
            collection.Add(boundView);

            if (Validator.IsRequired(ViewModel, sourcePath))
            {
                if (boundView.Hint.IsNotNullOrEmpty() && !boundView.Hint.EndsWith("*"))
                    boundView.Hint += "*";
            }
        }

        protected override object GetTargetFromBinding(object targetBinding)
        {
            object boundView = null;
            var textBinding = targetBinding as MvxTextViewTextTargetBinding;
            if (textBinding != null)
            {
                if (TextViewGetter != null)
                {
                    boundView = TextViewGetter.GetValue(textBinding, null);
                }
                else
                {
                    MvxTrace.Error("TextView on {0} cannot be found.", typeof(MvxTextViewTextTargetBinding));
                }
            }
            return boundView;
        }
    }
}