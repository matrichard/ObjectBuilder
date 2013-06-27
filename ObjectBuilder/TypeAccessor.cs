using System;
using System.Collections.Generic;

namespace Builder
{
    public static class TypeAccessor
    {
        public static IDictionary<string, Accessor<T>> Map<T>()
        {
            var accessors = new Dictionary<string, Accessor<T>>();
            var props = typeof (T).GetProperties();
            foreach (var propertyInfo in props)
            {
                var getter = PropertyAccessor.CreateGet<T>(propertyInfo);
                var setter = PropertyAccessor.CreateSet<T>(propertyInfo);
                
                accessors.Add(propertyInfo.Name, new Accessor<T>(getter, setter));
            }

            return accessors;
        }
    }

    public class Accessor<T>
    {
        public Func<T, object> Getter { get; private set; }
        public Action<T, object> Setter { get; private set; }

        public Accessor(Func<T, object> getter, Action<T, object> setter)
        {
            this.Getter = getter;
            this.Setter = setter;
        }
        

        
    }
}