using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectBuilderTests
{
    [TestClass]
    public class ObjectBuilderTests
    {
        [TestMethod]
        public void GivenObjectBuilderThenBuildFromDtoThenEntityNotNull()
        {
            // ARRANGE
            var dto = new Dto();
            var sut = new ObjectBuilder();

            // ACT
            var e = sut.Build<Dto,Entity>(dto);
            
            // ASSERT
            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void GivenObjectBuilderThenBuildFromDtoThenEntityPropertiesShouldBeEquals()
        {
            // ARRANGE
            var dto = new Dto
                {
                    StringProperty = "a string value",
                    DecimalProperty = 0.00M,
                    IntProperty = 10
                };
            var sut = new ObjectBuilder();

            // ACT
            var e = sut.Build<Dto,Entity>(dto);

            // ASSERT
            Assert.AreEqual(dto.StringProperty, e.StringProperty);
            Assert.AreEqual(dto.DecimalProperty, e.DecimalProperty);
            Assert.AreEqual(dto.IntProperty, e.IntProperty);
        }
    }

    public class ObjectBuilder
    {
        public TOut Build<TIn,TOut>(TIn dto) 
            where TOut : new()
        {
            var o = new TOut();
            return o;
        }
    }
}
