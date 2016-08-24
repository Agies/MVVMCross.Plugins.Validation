using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCross.Plugins.Validation.ForFieldBinding
{
    public class NCFieldCollectionCountValidation : IValidation
    {
        private readonly Func<int, bool> _predicate;
        private string _message;
        private object _maximum;
        private object _minimum;

        public NCFieldCollectionCountValidation(Func<int, bool> predicate, object minimum, object maximum, string message)
        {
            _predicate = predicate;
            _minimum = minimum;
            _maximum = maximum;
            _message = message;
        }

        public IErrorInfo Validate(string fieldName, object value, object subject)
        {
            if (value == null)
                return null;

            var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
            return Validate(fieldName, incValue as ICollection, subject);
        }

        public IErrorInfo Validate(string fieldName, ICollection value, object subject)
        {
            if (!_predicate(value?.Count ?? 0))
            {
                return new ErrorInfo(fieldName, _message == null ?
                    string.Format("The Count of {0} must between {1} and {2}", fieldName, _minimum, _maximum) :
                    string.Format(_message, fieldName, _minimum, _maximum)
                    );
            }
            return null;
        }
    }
}
