using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Converters
{
    [TestFixture]
    internal class PositionConverterTests
    {
        [Test]
        public void Can_Convert_Position()
        {
            var positionConverter = new PositionConverter();
            
            var result = positionConverter.CanConvert(typeof(Position));

            Assert.IsTrue(result);
        }

        [Test]
        public void Cannot_Convert_Point()
        {
            var positionConverter = new PositionConverter();

            var result = positionConverter.CanConvert(typeof(Point));

            Assert.IsFalse(result);
        }
    }
}