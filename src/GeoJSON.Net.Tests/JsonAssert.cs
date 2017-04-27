using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Tests
{
    /// <summary>
    ///     Assertions for json strings
    /// </summary>
    public static class JsonAssert
    {
        /// <summary>
        ///     Asserts that the json strings are equal.
        /// </summary>
        /// <remarks>
        ///     Parses each json string into a <see cref="JObject" />, sorts the properties of each
        ///     and then serializes each back to a json string for comparison.
        /// </remarks>
        /// <param name="expectJson">The expect json.</param>
        /// <param name="actualJson">The actual json.</param>
        public static void AreEqual(string expectJson, string actualJson)
        {
            Assert.AreEqual(
                JObject.Parse(expectJson).SortProperties().ToString(),
                JObject.Parse(actualJson).SortProperties().ToString());
        }

        /// <summary>
        ///     Asserts that <paramref name="actualJson" /> contains <paramref name="expectedJson" />
        /// </summary>
        /// <param name="expectedJson">The expected json.</param>
        /// <param name="actualJson">The actual json.</param>
        public static void Contains(string expectedJson, string actualJson)
        {
            Assert.IsTrue(actualJson.Contains(expectedJson), "expected {0} to contain {1}", actualJson, expectedJson);
        }

        /// <summary>
        ///     Sorts the properties of a JObject
        /// </summary>
        /// <param name="jObject">The json object whhose properties to sort</param>
        /// <returns>A new instance of a <see cref="JObject" /> with sorted properties</returns>
        private static JObject SortProperties(this JObject jObject)
        {
            var result = new JObject();

            foreach (var property in jObject.Properties().OrderBy(p => p.Name))
            {
                var value = property.Value as JObject;

                if (value != null)
                {
                    value = value.SortProperties();
                    result.Add(property.Name, value);
                }
                else
                {
                    result.Add(property.Name, property.Value);
                }
            }

            return result;
        }

        public static void AssertCoordinates(string geojson, int expectedNesting, IEnumerable<object> coords)
        {
            var coordMatch = Regex.Matches(geojson, "\"coordinates\":(.+?)(,\\s*\"|})");
            Assert.AreEqual(1, coordMatch.Count);
            var deserializedCoords = JsonConvert.DeserializeObject<JArray>(coordMatch[0].Groups[1].Value);
            AssertCoordInternal(deserializedCoords, expectedNesting, coords);
        }

        public static void AssertCoordInternal(JArray coords, int expectedNesting, IEnumerable<object> expectedCoords)
        {
            Assert.IsTrue(expectedNesting >= 0);

            if (expectedNesting == 0)
            {
                AssertCoordinate((GeographicPosition)expectedCoords.First(), coords);
            }
            else
            {
                var enumerator = expectedCoords.GetEnumerator();
                var moveNext = enumerator.MoveNext();
                var i = 0;

                foreach (var deserializedCoord in coords)
                {
                    Assert.IsTrue(moveNext);
                    var array = deserializedCoord as JArray;
                    if (array != null && array.Count > 0 && !(array[0] is JValue))
                    {
                        AssertCoordInternal(array, expectedNesting - 1, (IEnumerable<object>)enumerator.Current);
                    }
                    else if (array != null)
                    {
                        var expectedCoord = (GeographicPosition)enumerator.Current;
                        AssertCoordinate(expectedCoord, array);
                    }

                    moveNext = enumerator.MoveNext();
                    i++;
                }
            }
        }

        public static void AssertCoordinate(GeographicPosition expectedCoord, JArray array)
        {
            Assert.AreEqual(expectedCoord.Latitude, (double)array[1], 1e-6);
            Assert.AreEqual(expectedCoord.Longitude, (double)array[0], 1e-6);
        }
    }
}