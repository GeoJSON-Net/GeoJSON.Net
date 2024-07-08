using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace GeoJSON.Net.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class LinkedCRSTests : TestBase
    {
        private const string Href = "http://localhost";

        [Test]
        public void Has_Correct_Type()
        {
            var crs = new LinkedCRS(Href);
            ClassicAssert.AreEqual(CRSType.Link, crs.Type);
        }

        [Test]
        public void Has_Href_Property_With_Href()
        {
            var crs = new LinkedCRS(Href);

            ClassicAssert.IsTrue(crs.Properties.ContainsKey("href"));
            ClassicAssert.AreEqual(Href, crs.Properties["href"]);
        }

        [Test]
        public void Has_Type_Property()
        {
            const string type = "ogcwkt";
            var crs = new LinkedCRS(Href, type);

            ClassicAssert.IsTrue(crs.Properties.ContainsKey("type"));
            ClassicAssert.AreEqual(type, crs.Properties["type"]);
        }

        [Test]
        public void Can_Serialize()
        {
            var collection = new Point(new Position(1, 2, 3)) { CRS = new LinkedCRS(Href) };
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.Contains("{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}", actualJson);
        }

        [Test]
        public void Can_Deserialize_CRS_issue_101()
        {
            const string pointJson = "{\"type\":\"Point\",\"coordinates\":[2.0,1.0,3.0],\"crs\":{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}}";
            var pointWithCRS = JsonConvert.DeserializeObject<Point>(pointJson);
            var linkCRS = pointWithCRS.CRS as LinkedCRS;

            ClassicAssert.IsNotNull(linkCRS);
            ClassicAssert.AreEqual(CRSType.Link, linkCRS.Type);
            ClassicAssert.AreEqual(Href, linkCRS.Properties["href"]);
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Href_String_Is_Null()
        {
            ClassicAssert.Throws<ArgumentNullException>(() => { var crs = new LinkedCRS((string)null); });
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Href_Uri_Is_Null()
        {
            ClassicAssert.Throws<ArgumentNullException>(() => { var crs = new LinkedCRS((Uri)null); });
        }

        [Test]
        public void Ctor_Throws_ArgumentExpection_When_Href_Is_Not_Dereferencable_Uri()
        {
#if NETFRAMEWORK
            var expected = $"must be a dereferenceable URI{Environment.NewLine}Parameter name: href";
#else
            var expected = $"must be a dereferenceable URI (Parameter 'href')";
#endif
#if NETCOREAPP1_1
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
#else
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
#endif

            var argumentExpection = ClassicAssert.Throws<ArgumentException>(() => { var crs = new LinkedCRS("http://not-a-valid-<>-url"); });
            ClassicAssert.AreEqual(expected, argumentExpection.Message);
        }

        [Test]
        public void Ctor_Does_Not_Throw_When_Href_Is_Dereferencable_Uri()
        {
            ClassicAssert.DoesNotThrow(() => { var crs = new LinkedCRS("data.crs"); });
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Name_Is_Empty()
        {
            ClassicAssert.Throws<ArgumentException>(() => { var crs = new LinkedCRS(string.Empty); });
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var left = new LinkedCRS(Href);
            var right = new LinkedCRS(Href);

            ClassicAssert.AreEqual(left, right);

            ClassicAssert.IsTrue(left == right);
            ClassicAssert.IsTrue(right == left);

            ClassicAssert.IsTrue(left.Equals(right));
            ClassicAssert.IsTrue(right.Equals(left));

            ClassicAssert.IsTrue(left.Equals(left));
            ClassicAssert.IsTrue(right.Equals(right));

            ClassicAssert.AreEqual(left.GetHashCode(), right.GetHashCode());

            right = new LinkedCRS(Href + "?query=null");

            ClassicAssert.AreNotEqual(left, right);

            ClassicAssert.IsFalse(left == right);
            ClassicAssert.IsFalse(right == left);

            ClassicAssert.IsFalse(left.Equals(right));
            ClassicAssert.IsFalse(right.Equals(left));

            ClassicAssert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}