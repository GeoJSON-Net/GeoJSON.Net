using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace GeoJSON.Net.Tests
{
    [TestFixture]
    public class FeatureTests : TestBase
    {
        [Test]
        public void FeatureSerialization()
        {
            var coordinates = new[]
            {
                new List<IPosition>
                {
                    new GeographicPosition(52.370725881211314, 4.889259338378906),
                    new GeographicPosition(52.3711451105601, 4.895267486572266),
                    new GeographicPosition(52.36931095278263, 4.892091751098633),
                    new GeographicPosition(52.370725881211314, 4.889259338378906)
                },
                new List<IPosition>
                {
                    new GeographicPosition(52.370725881211314, 4.989259338378906),
                    new GeographicPosition(52.3711451105601, 4.995267486572266),
                    new GeographicPosition(52.36931095278263, 4.992091751098633),
                    new GeographicPosition(52.370725881211314, 4.989259338378906)
                },
            };

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            IGeometryObject geometry;

            geometry = new LineString(coordinates[0]);
            JsonAssert.AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), DefaultJsonSerializerSettings), 1, coordinates[0]);

            geometry = new Point(coordinates[0][0]);
            JsonAssert.AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), DefaultJsonSerializerSettings), 0, coordinates[0].Take(1).ToArray());

            geometry = new MultiLineString(coordinates.Select(ca => new LineString(ca)).ToList());
            JsonAssert.AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), DefaultJsonSerializerSettings), 2, coordinates);

            geometry = new Polygon(coordinates.Select(ca => new LineString(ca)).ToList());
            JsonAssert.AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), DefaultJsonSerializerSettings), 2, coordinates);
        }

        /// <summary>
        ///     Serializes the whole Polygon with properties
        /// </summary>
        [Test]
        public void PointFeatureSerialization()
        {
            var point = new Point(new GeographicPosition(45.79012, 15.94107));
            var featureProperties = new Dictionary<string, object> { { "Name", "Foo" } };
            var model = new Feature.Feature(point, featureProperties);
            var serializedData = JsonConvert.SerializeObject(model);

            Assert.IsFalse(serializedData.Contains("longitude"));
        }

        [Test]
        public void PolygonFeatureSerialization()
        {
            var coordinates = new List<GeographicPosition>
            {
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.3711451105601, 4.895267486572266),
                new GeographicPosition(52.36931095278263, 4.892091751098633),
                new GeographicPosition(52.370725881211314, 4.889259338378906)
            };

            var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
            var featureProperties = new Dictionary<string, object> { { "Name", "Foo" } };
            var model = new Feature.Feature(polygon, featureProperties);

            var serializedData = JsonConvert.SerializeObject(model);
        }
    }

    [TestFixture]
    public class FeatureCollectionTests : TestBase
    {
        [Test]
        public void FeatureCollectionSerialization()
        {
            var model = new FeatureCollection(null);
            for (var i = 10; i-- > 0;)
            {
                var geom = new LineString(new[]
                {
                    new GeographicPosition(51.010, -1.034),
                    new GeographicPosition(51.010, -0.034),
                });

                var props = new Dictionary<string, object>();
                props.Add("test1", "1");
                props.Add("test2", 2);

                var feature = new Feature.Feature(geom, props);
                model.Features.Add(feature);
            }

            var serialized = JsonConvert.SerializeObject(model, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            Assert.IsNotNull(serialized);
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
        }


    }
}