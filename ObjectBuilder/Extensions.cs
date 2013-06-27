using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Builder
{
    public static class Extensions
    {
        public static PropertyInfo GetProperty<T, TProp>(this Type t, Expression<Func<T, TProp>> action)
        {
            //var expr = (MemberExpression) action.Body;
            //var propName = expr.Member.Name;
            var propName = action.PropertyName();

            return t.GetProperty(propName);
        }

        public static string PropertyName<T, TProp>(this Expression<Func<T, TProp>> action)
        {
            var expr = (MemberExpression)action.Body;
            return expr.Member.Name;
        }

        public static string ToInvariantString(this object value)
        {
            var provider = CultureInfo.InvariantCulture;
            return Convert.ToString(value, provider);
        }
    }
}
