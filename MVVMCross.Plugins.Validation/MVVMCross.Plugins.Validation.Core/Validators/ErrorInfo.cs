namespace MVVMCross.Plugins.Validation
{
    public class ErrorInfo : IErrorInfo
    {
        public ErrorInfo(string propertyName, string message)
        {
            Message = message;
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
        public string Message { get; private set; }
    }
}