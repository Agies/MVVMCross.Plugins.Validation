using MvvmCross.FieldBinding;
using System;
using System.Linq.Expressions;

namespace MVVMCross.Plugins.Validation.ForFieldBinding
{
    public class NCFieldRequiredAttribute : NCFieldValidationAttribute
    {
        public NCFieldRequiredAttribute(string message = null)
            : base(message)
        {
        }

        public override IValidation CreateValidation(Type valueType)
        {
            if (valueType.Name != "INC`1" || valueType.GenericTypeArguments.Length != 1)
                throw new NotSupportedException("NCFieldRequired Validator for type " + valueType.Name + " is not supported.");

            var genericityType = valueType.GenericTypeArguments[0];
            if (genericityType == null || genericityType == typeof(string))
                return new NCFieldRequiredValidation<INC<string>>(v => v == null || v.Value.IsNotNullOrEmpty(), Message);

            if (!genericityType.IsByRef)
            {
                var parameterExpresssion = Expression.Parameter(genericityType, "o");
                var functionType = typeof(Func<,>).MakeGenericType(new[] { genericityType, typeof(bool) });
                var function = Expression.Lambda(
                    functionType,
                    Expression.NotEqual(
                        parameterExpresssion,
                        Expression.Constant(Activator.CreateInstance(genericityType))),
                    parameterExpresssion).Compile();
                return (IValidation)Activator.CreateInstance(typeof(NCFieldRequiredValidation<>).MakeGenericType(new[] { genericityType }), function, Message);
            }
            if (genericityType.IsByRef)
                return new NCFieldRequiredValidation<object>(o => o != null, Message);
            throw new NotSupportedException("NCFieldRequired Validator for type " + genericityType.Name + " is not supported.");
        }
    }
}