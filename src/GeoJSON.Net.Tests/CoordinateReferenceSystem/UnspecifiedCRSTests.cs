using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class UnspecifiedCRSTests : TestBase
    {
        [Test]
        public void Has_Correct_Type()
        {
            var crs = new UnspecifiedCRS();

            Assert.That(crs.Type, Is.EqualTo(CRSType.Unspecified));
        }

        [Test]
        public void Can_Serialize_To_Null()
        {
            var collection = new FeatureCollection { CRS = new UnspecifiedCRS() };
            var expectedJson = "{\"type\":\"FeatureCollection\",\"crs\":null,\"features\":[] }";
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_From_Null()
        {
            var json = "{\"type\":\"FeatureCollection\",\"crs\":null,\"features\":[] }";
            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

            Assert.That(featureCollection.CRS, Is.InstanceOf<UnspecifiedCRS>());
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var left = new UnspecifiedCRS();
            var right = new UnspecifiedCRS();

            Assert.That(right, Is.EqualTo(left));

            Assert.That(left == right);
            Assert.That(right == left);

            Assert.That(left.Equals(right));
            Assert.That(right.Equals(left));

            Assert.That(left.Equals(left));
            Assert.That(right.Equals(right));

            Assert.That(right.GetHashCode(), Is.EqualTo(left.GetHashCode()));
        }
    }
}