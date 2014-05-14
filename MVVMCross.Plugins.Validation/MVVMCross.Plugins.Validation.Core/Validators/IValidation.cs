namespace MVVMCross.Plugins.Validation
{
    public interface IValidation
    {
        IErrorInfo Validate(string propertyName, object value, object subject);
    }

    public interface IValidation<in T> : IValidation
    {
        IErrorInfo Validate(string propertyName, T value, object subject);
    }
}