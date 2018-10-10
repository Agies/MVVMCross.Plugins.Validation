namespace MvvmCross.Plugin.Validation
{
    public class ErrorInfo : IErrorInfo
    {
        public ErrorInfo(string memberName, string message)
        {
            Message = message;
            MemberName = memberName;
        }

        public string MemberName { get; private set; }
        public string Message { get; private set; }
    }
}