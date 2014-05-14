namespace MVVMCross.Plugins.Validation
{
    public interface IErrorInfo
    {
        string PropertyName { get; }
        string Message { get; }
    }
}