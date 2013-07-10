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
            var propName = action.PropertyName();

            return t.GetProperty(propName);
        }

        public static string PropertyName<T, TProp>(this Expression<Func<T, TProp>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                return ((MemberExpression)op).Member.Name;
            }
        }

        public static string ToInvariantString(this object value)
        {
            var provider = CultureInfo.InvariantCulture;
            return Convert.ToString(value, provider);
        }
    }
}
