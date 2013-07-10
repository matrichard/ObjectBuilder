using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Builder
{
    public class ObjectBuilder<TSource,TDestination> 
        where TDestination : new()
    {
        public TDestination Build(TSource input)
        {
            var objectMapper = new ObjectMapper<TSource, TDestination>();
            return objectMapper.InjectFrom(input);
        }
    }

    public class ObjectMapper<TSource, TDestination>
        where TDestination : new()
    {
        private readonly IDictionary<string, PropertyAssignement<TSource, TDestination>> assignements =
            new Dictionary<string, PropertyAssignement<TSource, TDestination>>();

        private readonly IDictionary<string, Accessor<TDestination>> destinationAccessors;

        public ObjectMapper()
        {
            destinationAccessors = TypeAccessor.Map<TDestination>();
        }

        public TDestination InjectFrom(TSource source)
        {
            var destination = new TDestination();
            
            InjectAssignements(source, destination);
            InjectValues(source, destination);
            
            return destination;
        }

        private void InjectAssignements(TSource source, TDestination destination)
        {
            foreach (var assignement in assignements.Select(propertyAssignement => propertyAssignement.Value))
            {
                assignement.DestinationSetter(destination, assignement.SourceGetter(source));
            }
        }

        private void InjectValues(TSource source, TDestination destination)
        {
            var sourceAccessors = TypeAccessor.Map<TSource>();
            var keys = destinationAccessors.Keys;
            foreach (var key in keys)
            {
                Accessor<TSource> accessor;
                if (sourceAccessors.TryGetValue(key, out accessor))
                {
                    destinationAccessors[key].Setter(destination, accessor.Getter(source));
                }
            }
        }

        public void AssignProperty(Expression<Func<TDestination, object>> destinationProperty, Func<TSource, object> sourceGetter)
        {
            var propertyName = destinationProperty.PropertyName();
            var accessor = GetAccessor(propertyName);
            if (accessor != null)
            {
                var assignement = new PropertyAssignement<TSource, TDestination>(propertyName, sourceGetter, accessor.Setter);
                assignements.Add(assignement.PropertyName, assignement);
                destinationAccessors.Remove(propertyName);
            }
            //TODO : check in assignement it may be an overide of a previous assignement
            
        }

        private Accessor<TDestination> GetAccessor(string propertyName)
        {
            Accessor<TDestination> accessor;
            destinationAccessors.TryGetValue(propertyName, out accessor);
            return accessor;
        }
    }

    public class PropertyAssignement<TSource, TDestination>
    {
        public string PropertyName { get; private set; }
        public Func<TSource, object> SourceGetter { get; private set; }
        public Action<TDestination,object> DestinationSetter { get; private set; }

        public PropertyAssignement(string propertyName, Func<TSource, object> sourcePropertyGetter, Action<TDestination, object> destinationSetter)
        {
            PropertyName = propertyName;
            SourceGetter = sourcePropertyGetter;
            DestinationSetter = destinationSetter;
        }

    }
}