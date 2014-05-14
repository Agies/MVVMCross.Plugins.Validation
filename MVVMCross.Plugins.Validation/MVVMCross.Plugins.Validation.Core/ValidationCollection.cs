using System.Collections.ObjectModel;

namespace MVVMCross.Plugins.Validation
{
    public class ValidationCollection : Collection<IValidationInfo>, IValidationCollection
    {
    }
}