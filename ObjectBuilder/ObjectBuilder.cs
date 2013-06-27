using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Builder
{
    public class ObjectBuilder<TIn,TOut> 
        where TOut : new()
    {
        private readonly IDictionary<string, Func<TIn, object>> Assignements = new Dictionary<string, Func<TIn, object>>(); 

        public TOut Build(TIn input) 
        {
            var o = new TOut();
            var sourceAccessors = TypeAccessor.Map<TIn>();
            var destinationAccessors = TypeAccessor.Map<TOut>();

            var keys = destinationAccessors.Keys;
            foreach (var key in keys)
            {
                Accessor<TIn> accessor;
                Func<TIn, object> getter;
                if (sourceAccessors.TryGetValue(key, out accessor))
                {
                    getter = accessor.Getter;
                }
                else
                {
                    Assignements.TryGetValue(key, out getter);
                }

                if (getter != null)
                {
                    destinationAccessors[key].Setter(o, getter(input));
                }
                
            }

            return o;
        }

        public void Assign(Expression<Func<TOut, object>> destinationProperty, Func<TIn, object> sourceProperty)
        {
            Assignements.Add(destinationProperty.PropertyName(), sourceProperty);
        }
    }
}