using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBuilder
{
    public static class Extensions
    {
        public static PropertyInfo GetProperty<T, TProp>(this Type t, Expression<Func<T, TProp>> action)
        {
            var expr = (MemberExpression) action.Body;
            var propName = expr.Member.Name;

            return t.GetProperty(propName);
        }

        public static string ToInvariantString(this object value)
        {
            var provider = CultureInfo.InvariantCulture;
            return Convert.ToString(value, provider);
        }
    }
}
