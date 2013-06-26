using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectBuilder;

namespace ObjectBuilderTests
{
    [TestClass]
    public class ObjectAccessorTests
    {
        [TestCategory("TypeAccessor")]
        [TestMethod]
        public void GivenObjectAccessorWhenMapShouldNotBeNull()
        {
            // ARRANGE
            

            // ACT
            var def = TypeAccessor.Map();

            // ASSERT
            Assert.IsNotNull(def);
        }

        //GivenObjectAccessorOfEntityWhenGetStringPropertyGetAccessorShouldNotBeNull()
    }
}
