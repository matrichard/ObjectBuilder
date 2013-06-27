namespace ObjectBuilderTests
{
    public class Entity
    {
        public string StringProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public int IntProperty { get; set; }

        //public DateTime DateTimeProperty { get; set; }
    }

    public class Entity2
    {
        public string StringProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public int IntProperty { get; set; }

        //public DateTime DateTimeProperty { get; set; }
    }

    public class Dto
    {
        public string StringProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public int IntProperty { get; set; }
    }

    public class Dto2
    {
        public string PropertyString { get; set; }
        public decimal PropertyDecimal { get; set; }
        public int IntProperty { get; set; }
    }
}