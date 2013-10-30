namespace MVVMCross.Plugins.Validation.Core
{
    public interface IValidator
    {
        IErrorCollection Validate(object subject, string group = null);
        bool IsRequired(object subject, string propertyName);
    }
}