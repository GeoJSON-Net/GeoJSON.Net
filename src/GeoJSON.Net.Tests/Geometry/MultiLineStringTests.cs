using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiLineStringTests : TestBase
    {
        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson();

            var expectedMultiLineString = new MultiLineString(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new GeographicPosition(52.379790828551016, 5.3173828125),
                    new GeographicPosition(52.36721467920585, 5.456085205078125),
                    new GeographicPosition(52.303440474272755, 5.386047363281249, 4.23)
                }),
                new LineString(new List<IPosition>
                {
                    new GeographicPosition(52.379790828551016, 5.3273828125),
                    new GeographicPosition(52.36721467920585, 5.486085205078125),
                    new GeographicPosition(52.303440474272755, 5.426047363281249, 4.23)
                })
            });

            var multiLineString = JsonConvert.DeserializeObject<MultiLineString>(json);

            Assert.IsNotNull(multiLineString);
            Assert.AreEqual(expectedMultiLineString, multiLineString);
        }

        [Test]
        public void Can_Serialize()
        {
            var expectedMultiLineString = new MultiLineString(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new GeographicPosition(52.379790828551016, 5.3173828125),
                    new GeographicPosition(52.36721467920585, 5.456085205078125),
                    new GeographicPosition(52.303440474272755, 5.386047363281249, 4.23)
                }),
                new LineString(new List<IPosition>
                {
                    new GeographicPosition(52.379790828551016, 5.3273828125),
                    new GeographicPosition(52.36721467920585, 5.486085205078125),
                    new GeographicPosition(52.303440474272755, 5.426047363281249, 4.23)
                })
            });

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(expectedMultiLineString);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}