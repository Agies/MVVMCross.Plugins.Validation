﻿namespace MVVMCross.Plugins.Validation
{
    public interface IErrorInfo
    {
        string MemberName { get; }
        string Message { get; }
    }
}