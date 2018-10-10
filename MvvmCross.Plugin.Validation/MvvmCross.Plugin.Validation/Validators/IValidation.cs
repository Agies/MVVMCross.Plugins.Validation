namespace MvvmCross.Plugin.Validation
{
    public interface IValidation
    {
        IErrorInfo Validate(string memberName, object value, object subject);
    }

    public interface IValidation<in T> : IValidation
    {
        IErrorInfo Validate(string memberName, T value, object subject);
    }
}