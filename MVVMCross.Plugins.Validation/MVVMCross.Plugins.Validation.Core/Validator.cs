using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMCross.Plugins.Validation.Core
{
    public class Validator : IValidator
    {
        static readonly object SyncLock = new object();
        static readonly Dictionary<Type, IValidationCollection> ValidationCache = new Dictionary<Type, IValidationCollection>();
        public IValidationCollection Initialize(object subject)
        {
            IValidationCollection collection;
            var type = subject.GetType();
            lock (SyncLock)
            {
                if (!ValidationCache.TryGetValue(type, out collection))
                {
                    collection = BuildCollectionFor(type);
                    ValidationCache[type] = collection;
                }
            }
            return collection;
        }

        private IValidationCollection BuildCollectionFor(Type type)
        {
            var validationCollection = new ValidationCollection();
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attributes = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>().ToArray();
                foreach (var validationAttribute in attributes)
                {
                    validationCollection.Add(
                        new ValidationInfo(propertyInfo, validationAttribute.CreateValidation(propertyInfo.PropertyType), validationAttribute.Groups));
                }
            }
            return validationCollection;
        }

        public IErrorCollection Validate(object subject, string group = null)
        {
            var collection = Initialize(subject);
            var result = collection
                .Where(c => c.Groups.IsNullOrEmpty() || c.Groups.Contains(group))
                .Select(c => c.Validation.Validate(c.Property.Name, c.Property.GetValue(subject, null), subject))
                .Where(t => t != null);
            return new ErrorCollection(result.ToList());
        }

        public bool IsRequired(object subject, string propertyName)
        {
            return Initialize(subject).Any(v => v.Property.Name == propertyName && HasRequiredValidator(v.Validation.GetType()));
        }

        private bool HasRequiredValidator(Type validationType)
        {
            var isGenericType = validationType.IsGenericType;
            if (isGenericType)
                return validationType.GetGenericTypeDefinition() == typeof (RequiredValidation<>);
            return false;
        }
    }
}