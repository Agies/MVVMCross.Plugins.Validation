namespace MvvmCross.Plugin.Validation
{
    public class ShouldBeLongValidation : IValidation
    {
        private readonly string _message;

        public ShouldBeLongValidation(string message)
        {
            _message = message;
        }

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            long result;
            if (value == null || long.TryParse(value.ToString(), out result))
                return null;
            return new ErrorInfo(propertyName, _message ?? string.Format("{0} should be a valid number.", propertyName));
        }
    }
}