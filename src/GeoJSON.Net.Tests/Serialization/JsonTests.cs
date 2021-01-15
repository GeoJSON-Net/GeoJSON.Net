using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Serialization
{
    public class JsonTests
    {
        [Test]
        public void Can_Serialize_as_GeoJSONObject()
        {
            GeoJSONObject source = new Point(new Position(10, 20));

            var json = JsonConvert.SerializeObject(source, new GeoJsonConverter());

            var deserialized = JsonConvert.DeserializeObject<GeoJSONObject>(json, new GeoJsonConverter());

            Assert.AreEqual(source, deserialized);
        }

        [Test]
        public void Can_Serialize_as_IGeoJSONObject()
        {
            IGeoJSONObject source = new Point(new Position(10, 20));

            var json = JsonConvert.SerializeObject(source, new GeoJsonConverter());

            var deserialized = JsonConvert.DeserializeObject<IGeoJSONObject>(json, new GeoJsonConverter());

            Assert.AreEqual(source, deserialized);
        }
    }
}
