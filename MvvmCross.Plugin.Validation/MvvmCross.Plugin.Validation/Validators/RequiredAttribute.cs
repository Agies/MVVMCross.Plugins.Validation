using System;
using System.Linq.Expressions;

namespace MvvmCross.Plugin.Validation
{
    public class RequiredAttribute : ValidationAttribute
    {
        public RequiredAttribute(string message = null)
            : base(message)
        {
        }

        public override IValidation CreateValidation(Type valueType)
        {
            if (valueType == typeof(string))
                return new RequiredValidation<string>(v => v.IsNotNullOrEmpty(), Message);
            if (!valueType.IsByRef)
            {
                var parameterExpresssion = Expression.Parameter(valueType, "o");
                var functionType = typeof(Func<,>).MakeGenericType(new[] { valueType, typeof(bool) });
                var function = Expression.Lambda(
                    functionType,
                    Expression.NotEqual(
                        parameterExpresssion,
                        Expression.Constant(Activator.CreateInstance(valueType))),
                    parameterExpresssion).Compile();
                return (IValidation)Activator.CreateInstance(typeof(RequiredValidation<>).MakeGenericType(new[] { valueType }), function, Message);
            }
            if (valueType.IsByRef)
                return new RequiredValidation<object>(o => o != null, Message);
            throw new NotSupportedException("Required Validator for type " + valueType.Name + " is not supported.");
        }
    }
}