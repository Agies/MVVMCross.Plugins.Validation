﻿namespace MVVMCross.Plugins.Validation
{
    public interface IValidator
    {
        IErrorCollection Validate(object subject, string group = null);
        bool IsRequired(object subject, string propertyName);
    }
}