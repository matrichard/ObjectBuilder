using System;
using System.Linq;
using Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectBuilderTests
{
    [TestClass]
    public class TypeAccessorTests
    {
        [TestCategory("TypeAccessor")]
        [TestMethod]
        public void GivenTypeAccessorWhenMapShouldNotBeNull()
        {
            // ARRANGE
            
            // ACT
            var accessors = TypeAccessor.Map<Entity>();

            // ASSERT
            Assert.IsNotNull(accessors);
        }

        [TestCategory("TypeAccessor")]
        [TestMethod]
        public void GivenTypeAccessorOfEntityWhenMapShouldNotBeEmpty()
        {
            // ARRANGE

            // ACT
            var accessors = TypeAccessor.Map<Entity>();

            // ASSERT
            Assert.IsTrue(accessors.Any());
        }

        [TestCategory("TypeAccessor")]
        [TestMethod]
        public void GivenTypeAccessorOfEntityWhenMapThenPropertiesExists()
        {
            // ARRANGE
            var expectedKeys = typeof (Entity).GetProperties().Select(p => p.Name).OrderBy(x => x);

            // ACT
            var accessors = TypeAccessor.Map<Entity>();
            
            // ASSERT
            Assert.IsFalse(expectedKeys.Except(accessors.Keys.OrderBy(x => x)).Any());
        }

        [TestCategory("TypeAccessor")]
        [TestMethod]
        public void GivenTypeAccessorOfEntityWhenMapThenGetPropertyValueAreEqual()
        {
            // ARRANGE
            var e = new Entity { DecimalProperty = 10.03M, StringProperty = "a string", IntProperty = 1 };

            // ACT
            var accessors = TypeAccessor.Map<Entity>();

            // ASSERT
            Assert.AreEqual(e.DecimalProperty, accessors["DecimalProperty"].Getter(e));
            Assert.AreEqual(e.StringProperty, accessors["StringProperty"].Getter(e));
            Assert.AreEqual(e.IntProperty, accessors["IntProperty"].Getter(e));
        }
    }
}
