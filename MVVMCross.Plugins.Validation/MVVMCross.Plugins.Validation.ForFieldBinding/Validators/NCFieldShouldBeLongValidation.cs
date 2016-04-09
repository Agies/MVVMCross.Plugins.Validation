using System.Linq;
using System.Reflection;

namespace MVVMCross.Plugins.Validation
{
    public class NCFieldShouldBeLongValidation : IValidation
    {
        private readonly string _message;

        public NCFieldShouldBeLongValidation(string message)
        {
            _message = message;
        }

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            if (value == null)
                return null;

            var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
            long result;
            if (incValue == null || long.TryParse(incValue.ToString(), out result))
                return null;

            return new ErrorInfo(propertyName, _message ?? string.Format("{0} should be a valid number.", propertyName));
        }
    }
}