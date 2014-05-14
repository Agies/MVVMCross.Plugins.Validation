namespace MVVMCross.Plugins.Validation
{
    public abstract class ValidationBase<T> : IValidation<T>
    {
        public string Message { get; private set; }

        protected ValidationBase(string message)
        {
            Message = message;
        }

        public abstract IErrorInfo Validate(string propertyName, T value, object subject);

        public IErrorInfo Validate(string propertyName, object value, object subject)
        {
            return Validate(propertyName, (T) value, subject);
        }

        /// <summary>
        /// Combines the validator tag with the property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tag">Tag should read with PropertyName + " " + Tag.</param>
        /// <returns></returns>
        protected virtual string FormatMessage(string propertyName, string tag = null)
        {
            return string.Format("{0} {1}.", propertyName, tag ?? Message);
        }
    }
}