namespace MvvmCross.Plugins.Validation
{
    public interface IErrorInfo
    {
        string MemberName { get; }
        string Message { get; }
    }
}