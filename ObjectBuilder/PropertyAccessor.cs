using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace ObjectBuilder
{
    public static class PropertyAccessor
    {
        public static Func<T,object> CreateGet<T>(PropertyInfo propInfo)
        {
            var root = propInfo.DeclaringType;

            var param = Expression.Parameter(root, "_");
            var property = Expression.Property(param, propInfo.Name);
            var expression = Expression.Lambda<Func<T, object>>(property, param);
            return expression.Compile();
        }

        // TODO : is conversion necessary here or should it be moved higher in the chain..... therefore the value assigned is already of the proper type
        public static Action<T, object> CreateSet<T>(PropertyInfo propInfo, Expression<Func<object, object>> conversion = null)
        {
            var targetParam = Expression.Parameter(typeof(T), "_");
            var valueParam = Expression.Parameter(typeof(object), "value");

            MethodInfo mi = null;
            if (conversion != null)
            {
                mi = GetMethodInfo(conversion);
                
            }
            var right = Expression.Convert(valueParam, propInfo.PropertyType, mi);
            var left = Expression.Property(targetParam, propInfo.Name);

            var body = Expression.Assign(left, right);

            var expr = Expression.Lambda<Action<T, object>>(body, targetParam, valueParam);
            return expr.Compile();
        }

        private static MethodInfo GetMethodInfo(Expression<Func<object, object>> func)
        {
            return ((MethodCallExpression)func.Body).Method;
        }
    }
}