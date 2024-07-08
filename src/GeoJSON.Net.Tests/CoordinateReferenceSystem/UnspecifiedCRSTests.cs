using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace GeoJSON.Net.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class UnspecifiedCRSTests : TestBase
    {
        [Test]
        public void Has_Correct_Type()
        {
            var crs = new UnspecifiedCRS();

            ClassicAssert.AreEqual(CRSType.Unspecified, crs.Type);
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

            ClassicAssert.IsInstanceOf<UnspecifiedCRS>(featureCollection.CRS);
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var left = new UnspecifiedCRS();
            var right = new UnspecifiedCRS();

            ClassicAssert.AreEqual(left, right);

            ClassicAssert.IsTrue(left == right);
            ClassicAssert.IsTrue(right == left);

            ClassicAssert.IsTrue(left.Equals(right));
            ClassicAssert.IsTrue(right.Equals(left));

            ClassicAssert.IsTrue(left.Equals(left));
            ClassicAssert.IsTrue(right.Equals(right));

            ClassicAssert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}