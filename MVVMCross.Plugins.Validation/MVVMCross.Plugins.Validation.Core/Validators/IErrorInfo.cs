namespace MVVMCross.Plugins.Validation.Core
{
    public interface IErrorInfo
    {
        string PropertyName { get; }
        string Message { get; }
    }
}