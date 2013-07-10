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
        public void GivenObjectBuilderWhenBuildFromDtoThenEntityNotNull()
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
        public void GivenObjectBuilderWhenBuildFromDtoThenEntityPropertiesShouldBeEquals()
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
        public void GivenObjectBuilderWhenBuildWithDtoWherePropertyNameDifferThenEntityPropertyShouldNotBeEqual()
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
            var entity = sut.Build(dto);

            // ASSERT
            Assert.AreNotEqual(dto.PropertyString, entity.StringProperty);
        }
    }

    [TestClass]
    public class ObjectMapperTests
    {
        [TestCategory("ObjectMapper")]
        [TestMethod]
        public void GivenObjectMapperWhenInjectFromThenReturnedObjectIsNotNull()
        {
            // ARRANGE
            var dto = new Dto
            {
                StringProperty = "a string value",
                DecimalProperty = 10.03M,
                IntProperty = 10
            };
            var sut = new ObjectMapper<Dto, Entity>();

            // ACT
            var entity = sut.InjectFrom(dto);

            // ASSERT
            Assert.IsNotNull(entity);
        }

        [TestCategory("ObjectMapper")]
        [TestMethod]
        public void GivenObjectMapperWhenInjectFromThenReturnedObjectIsTypeOfEntity()
        {
            // ARRANGE
            var dto = new Dto
            {
                StringProperty = "a string value",
                DecimalProperty = 10.03M,
                IntProperty = 10
            };
            var sut = new ObjectMapper<Dto, Entity>();

            // ACT
            var entity = sut.InjectFrom(dto);

            // ASSERT
            Assert.IsTrue(entity is Entity);
        }

        [TestCategory("ObjectMapper")]
        [TestMethod]
        public void GivenObjectMapperWhenInjectFromThenReturnedObjectPropertiesAreEqual()
        {
            // ARRANGE
            var dto = new Dto
            {
                StringProperty = "a string value",
                DecimalProperty = 10.03M,
                IntProperty = 10
            };
            var sut = new ObjectMapper<Dto, Entity>();

            // ACT
            var entity = sut.InjectFrom(dto);

            // ASSERT
            Assert.AreEqual(dto.StringProperty, entity.StringProperty);
            Assert.AreEqual(dto.DecimalProperty, entity.DecimalProperty);
            Assert.AreEqual(dto.IntProperty, entity.IntProperty);
        }

        [TestCategory("ObjectMapper")]
        [TestMethod]
        public void GivenObjectMapperWhenInjectFromWithDifferentPropertyNamesThenReturnedObjectPropertiesAreEqual()
        {
            // ARRANGE
            var dto = new Dto2
            {
                PropertyString = "a string value",
                PropertyDecimal = 10.03M,
                IntProperty = 10
            };
            var sut = new ObjectMapper<Dto2, Entity>();
            sut.AssignProperty(entity => entity.StringProperty, d => d.PropertyString);
            sut.AssignProperty(entity => entity.DecimalProperty, d => d.PropertyDecimal);

            // ACT
            var e = sut.InjectFrom(dto);

            // ASSERT
            Assert.AreEqual(dto.PropertyString, e.StringProperty);
            Assert.AreEqual(dto.PropertyDecimal, e.DecimalProperty);
            Assert.AreEqual(dto.IntProperty, e.IntProperty);
        }


    }
}
