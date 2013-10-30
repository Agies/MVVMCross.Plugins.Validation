using System.Collections.ObjectModel;

namespace MVVMCross.Plugins.Validation.Core
{
    public class ValidationCollection : Collection<IValidationInfo>, IValidationCollection
    {
    }
}