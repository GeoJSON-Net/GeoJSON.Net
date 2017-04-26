using GeoJSON.Net.Converters;
using GeoJSON.Net.CoordinateReferenceSystem;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Converters
{
    [TestFixture]
    internal class CrsConverterTests
    {
        [Test]
        public void Can_Convert_ICRSObject()
        {
            var crsConverter = new CrsConverter();
            var result = crsConverter.CanConvert(typeof(ICRSObject));
            Assert.IsTrue(result);
        }

        [Test]
        public void Cannot_Convert_IGeoJSONObject()
        {
            var crsConverter = new CrsConverter();
            var result = crsConverter.CanConvert(typeof(IGeoJSONObject));
            Assert.IsFalse(result);
        }
    }
}