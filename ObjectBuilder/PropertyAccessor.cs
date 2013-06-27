using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Builder
{
    public static class PropertyAccessor
    {
        public static Func<T,object> CreateGet<T>(PropertyInfo propInfo)
        {
            var root = propInfo.DeclaringType;

            var param = Expression.Parameter(root, "_");
            var property = Expression.Convert(Expression.Property(param, propInfo.Name), typeof(object));
            var expression = Expression.Lambda<Func<T, object>>(property, param);
            return expression.Compile();
        }

        public static Action<T, object> CreateSet<T>(PropertyInfo propInfo)
        {
            var targetParam = Expression.Parameter(typeof(T), "_");
            var valueParam = Expression.Parameter(typeof(object), "value");

            var right = Expression.Convert(valueParam, propInfo.PropertyType);
            var left = Expression.Property(targetParam, propInfo.Name);

            var body = Expression.Assign(left, right);

            var expr = Expression.Lambda<Action<T, object>>(body, targetParam, valueParam);
            return expr.Compile();
        }
    }
}