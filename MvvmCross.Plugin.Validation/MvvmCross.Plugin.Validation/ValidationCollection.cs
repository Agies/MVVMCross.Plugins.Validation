using System.Collections.ObjectModel;
using MvvmCross.ViewModels;

namespace MvvmCross.Plugin.Validation
{
    public class ValidationCollection : Collection<IValidationInfo>, IValidationCollection
    {
    }
}