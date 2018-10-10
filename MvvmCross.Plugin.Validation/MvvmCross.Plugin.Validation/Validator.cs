using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvvmCross.Plugin.Validation
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
            var properties = type.GetRuntimeProperties();
            foreach (var propertyInfo in properties)
            {
                var attributes = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>().ToArray();
                foreach (var validationAttribute in attributes)
                {
                    validationCollection.Add(
                        new ValidationInfo(propertyInfo, validationAttribute.CreateValidation(propertyInfo.PropertyType), validationAttribute.Groups));
                }
            }
            var fields = type.GetRuntimeFields();
            foreach (var fieldInfo in fields)
            {
                var attributes = fieldInfo.GetCustomAttributes(true).OfType<ValidationAttribute>().ToArray();
                foreach (var validationAttribute in attributes)
                {
                    validationCollection.Add(
                        new ValidationInfo(fieldInfo, validationAttribute.CreateValidation(fieldInfo.FieldType), validationAttribute.Groups));
                }
            }
            return validationCollection;
        }

        public IErrorCollection Validate(object subject, string group = null)
        {
            var collection = Initialize(subject);
            var result = collection
                .Where(c => c.Groups.IsNullOrEmpty() || c.Groups.Contains(group))
                .Select(c => c.Validation.Validate(c.Member.Name, (c.Member as PropertyInfo)?.GetValue(subject, null) ?? (c.Member as FieldInfo)?.GetValue(subject), subject))
                .Where(t => t != null);
            return new ErrorCollection(result.ToList());
        }

        public bool IsRequired(object subject, string memberName)
        {
            return Initialize(subject).Any(v => v.Member.Name == memberName && HasRequiredValidator(v.Validation.GetType()));
        }

        private bool HasRequiredValidator(Type validationType)
        {
            var isGenericType = validationType.IsConstructedGenericType;
            if (isGenericType)
                return validationType.GetGenericTypeDefinition() == typeof(RequiredValidation<>);
            return false;
        }
    }
}