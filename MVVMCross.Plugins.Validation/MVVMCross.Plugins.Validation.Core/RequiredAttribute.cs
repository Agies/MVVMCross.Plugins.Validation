using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MVVMCross.Plugins.Validation.Core
{
    public class RequiredAttribute : ValidationAttribute
    {
        public RequiredAttribute(string message = null) : base(message)
        {
        }

        public override IValidation CreateValidation(Type valueType)
        {
            if (valueType == typeof (string))
                return new RequiredValidation<string>(v => v.IsNotNullOrEmpty(), Message);
            if (valueType.IsValueType)
            {
                var parameterExpresssion = Expression.Parameter(valueType, "o");
                var functionType = typeof (Func<,>).MakeGenericType(new[] {valueType, typeof (bool)});
                var function = Expression.Lambda(
                    functionType,
                    Expression.NotEqual(
                        parameterExpresssion,
                        Expression.Constant(Activator.CreateInstance(valueType))),
                    parameterExpresssion).Compile();
                return (IValidation)Activator.CreateInstance(typeof (RequiredValidation<>).MakeGenericType(new[] {valueType}),function, Message);
            }
            if (valueType.IsClass)
                return new RequiredValidation<object>(o => o != null, Message);
            throw new NotSupportedException("Required Validator for type " + valueType.Name + " is not supported.");
        }
    }

    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }

    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> s)
        {
            return s == null || !s.Any();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> s)
        {
            return !s.IsNullOrEmpty();
        }
    }
}