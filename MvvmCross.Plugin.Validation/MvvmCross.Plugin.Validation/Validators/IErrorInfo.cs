namespace MvvmCross.Plugin.Validation
{
    public interface IErrorInfo
    {
        string MemberName { get; }
        string Message { get; }
    }
}