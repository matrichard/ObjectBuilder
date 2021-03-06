﻿using System;
using System.Globalization;
using Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectBuilderTests
{
    [TestClass]
    public class PropertyAccessorTests
    {
        [TestCategory("PropertyAccessor")]
        [TestProperty("Accessor", "Get")]
        [TestMethod]
        public void GivenPropertyAccessorWhenCreateGetAccessorThenIsNotNull()
        {
            // ARRANGE
            var propInfo = typeof(Entity).GetProperty((Entity x) => x.DecimalProperty);

            // ACT
            var getter = PropertyAccessor.CreateGet<Entity>(propInfo);

            // ASSERT
            Assert.IsNotNull(getter);
        }

        [TestCategory("PropertyAccessor")]
        [TestProperty("Accessor", "Get")]
        [TestMethod]
        public void GivenEntityWhenCreateGetAccessorForStringPropertyThenStringPropertyIsReturnWhenInvoked()
        {
            // ARRANGE
            var e = new Entity { StringProperty = "a string property" };
            var propInfo = e.GetType().GetProperty((Entity x) => x.StringProperty);

            // ACT
            var getter = PropertyAccessor.CreateGet<Entity>(propInfo);

            // ASSERT
            Assert.AreEqual(e.StringProperty, getter(e));
        }

        [TestCategory("PropertyAccessor")]
        [TestProperty("Accessor", "Get")]
        [TestMethod]
        public void GivenEntityWhenCreateGetAccessorForStringPropertyThenValueIsString()
        {
            // ARRANGE
            var e = new Entity { StringProperty = "a string property" };
            var propInfo = e.GetType().GetProperty((Entity x) => x.StringProperty);

            // ACT
            var getter = PropertyAccessor.CreateGet<Entity>(propInfo);

            // ASSERT
            Assert.IsTrue(getter(e) is string);
        }

        [TestCategory("PropertyAccessor")]
        [TestProperty("Accessor", "Set")]
        [TestMethod]
        public void GivenPropertyAccessorWhenCreateSetAccessorThenIsNotNull()
        {
            // ARRANGE
            var propInfo = typeof(Entity).GetProperty((Entity x) => x.StringProperty);

            // ACT
            var setter = PropertyAccessor.CreateSet<Entity>(propInfo);

            // ASSERT
            Assert.IsNotNull(setter);
        }

        [TestCategory("PropertyAccessor")]
        [TestProperty("Accessor", "Set")]
        [TestMethod]
        public void GivenEntityWhenCreateSetAccessorForStringPropertyThenStringPropertyIsAssigned()
        {
            // ARRANGE
            var e = new Entity { StringProperty = "a string property" };
            var propInfo = e.GetType().GetProperty((Entity x) => x.StringProperty);
            const string newValue = "a new string property value !!!!";

            var setter = PropertyAccessor.CreateSet<Entity>(propInfo);
            
            // ACT
            setter(e, newValue);

            // ASSERT
            Assert.AreEqual(newValue, e.StringProperty);
        }
    }
}