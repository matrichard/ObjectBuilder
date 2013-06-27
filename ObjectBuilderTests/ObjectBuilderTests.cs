using System;
using Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectBuilderTests
{
    [TestClass]
    public class ObjectBuilderTests
    {
        [TestCategory("ObjectBuilder")]
        [TestMethod]
        public void GivenObjectBuilderThenBuildFromDtoThenEntityNotNull()
        {
            // ARRANGE
            var dto = new Dto();
            var sut = new ObjectBuilder<Dto, Entity>();

            // ACT
            var e = sut.Build(dto);
            
            // ASSERT
            Assert.IsNotNull(e);
        }

        [TestCategory("ObjectBuilder")]
        [TestMethod]
        public void GivenObjectBuilderThenBuildFromDtoThenEntityPropertiesShouldBeEquals()
        {
            // ARRANGE
            var dto = new Dto
                {
                    StringProperty = "a string value",
                    DecimalProperty = 10.03M,
                    IntProperty = 10
                };
            var sut = new ObjectBuilder<Dto, Entity>();

            // ACT
            var e = sut.Build(dto);

            // ASSERT
            Assert.AreEqual(dto.StringProperty, e.StringProperty);
            Assert.AreEqual(dto.DecimalProperty, e.DecimalProperty);
            Assert.AreEqual(dto.IntProperty, e.IntProperty);
        }
        [TestCategory("ObjectBuilder")]
        [TestMethod]
        public void GivenObjectBuilderWhenBuildFromDtoWithPropertyNameThatDifferThenEntityPropertiesShouldBeEquals()
        {
            // ARRANGE
            var dto = new Dto2
            {
                PropertyString = "a string value",
                PropertyDecimal = 10.03M,
                IntProperty = 10
            };
            var sut = new ObjectBuilder<Dto2, Entity>();
            
            // ACT
            sut.Assign(e => e.StringProperty, d => d.PropertyString);
            var entity = sut.Build(dto);

            // ASSERT
            Assert.AreEqual(dto.PropertyString, entity.StringProperty);
            Assert.AreEqual(dto.PropertyDecimal, entity.DecimalProperty);
            Assert.AreEqual(dto.IntProperty, entity.IntProperty);
        }

    }
}
