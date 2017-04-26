using GeoJSON.Net.Converters;
using GeoJSON.Net.CoordinateReferenceSystem;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Converters
{
    [TestFixture]
    internal class GeoJsonConverterTests
    {
        [Test]
        public void Can_Convert_IGeoJSONObject()
        {
            var geoJsonConverter = new GeoJsonConverter();
            var result = geoJsonConverter.CanConvert(typeof(IGeoJSONObject));
            Assert.IsTrue(result);
        }

        [Test]
        public void Cannot_Convert_ICRSObject()
        {
            var geoJsonConverter = new GeoJsonConverter();
            var result = geoJsonConverter.CanConvert(typeof(ICRSObject));
            Assert.IsFalse(result);
        }
    }
}