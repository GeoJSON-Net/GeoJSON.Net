using System;
using System.Linq;
using Microsoft.SqlServer.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlTypes;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.MsSqlSpatial.Tests
{
	[TestClass]
	public class ToGeoJSONGeographyTests
	{
		[TestMethod]
		public void ValidPointTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(simplePoint);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<Point>(simplePoint);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.Point);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.Point);
			Assert.IsNotNull(geoJSONobj.BoundingBoxes);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, simplePoint.BoundingBox());
		}

		[TestMethod]
		public void ValidMultiPointTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiPoint);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<MultiPoint>(multiPoint);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiPoint);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.MultiPoint);
			Assert.IsNotNull(geoJSONobj.BoundingBoxes);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, multiPoint.BoundingBox());
		}

		[TestMethod]
		public void ValidLineStringTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(lineString);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<LineString>(lineString);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.LineString);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.LineString);
			Assert.IsNotNull(geoJSONobj.BoundingBoxes);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, lineString.BoundingBox());
		}

		[TestMethod]
		public void ValidMultiLineStringTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiLineString);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<MultiLineString>(multiLineString);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiLineString);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.MultiLineString);
			Assert.IsNotNull(geoJSONobj.BoundingBoxes);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, multiLineString.BoundingBox());
		}

		[TestMethod]
		public void ValidPolygonTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(simplePoly);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<Polygon>(simplePoly);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.Polygon);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.Polygon);
			Assert.IsNotNull(geoJSONobj.BoundingBoxes);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, simplePoly.BoundingBox());


			geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(polyWithHole);
			geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<Polygon>(polyWithHole);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.Polygon);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.Polygon);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, polyWithHole.BoundingBox());
			Assert.AreEqual(geoJSONobj.Coordinates.Count, 2);
		}

		[TestMethod]
		public void ValidMultiPolygonTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiPolygon);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<MultiPolygon>(multiPolygon);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiPolygon);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.MultiPolygon);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, multiPolygon.BoundingBox());
			Assert.AreEqual(geoJSONobj.Coordinates.Count, 3);
			Assert.AreEqual(geoJSONobj.Coordinates[0].Coordinates.Count, 2);
			Assert.AreEqual(geoJSONobj.Coordinates[1].Coordinates.Count, 1);
			Assert.AreEqual(geoJSONobj.Coordinates[2].Coordinates.Count, 2);
		}

		[TestMethod]
		public void ValidGeometryCollectionTest_Geography()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(geomCol);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<GeometryCollection>(geomCol);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.GeometryCollection);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.GeometryCollection);
			Assert.IsTrue(geoJSONobj.BoundingBoxes.Length == 4);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, geomCol.BoundingBox());
			Assert.AreEqual(geoJSONobj.Geometries.Count, 3);
			Assert.AreEqual(geoJSONobj.Geometries[0].Type, GeoJSONObjectType.Polygon);
			Assert.AreEqual(geoJSONobj.Geometries[1].Type, GeoJSONObjectType.LineString);
			Assert.AreEqual(geoJSONobj.Geometries[2].Type, GeoJSONObjectType.Point);
		}

		#region Test geographies

		SqlGeography simplePoint = SqlGeography.Point(47, 1, 4326).MakeValidIfInvalid();
		SqlGeography multiPoint = SqlGeography.Parse(new SqlString("MULTIPOINT((47 1),(46 1),(46 0),(47 0),(47 1))")).MakeValidIfInvalid();
		SqlGeography lineString = SqlGeography.Parse(new SqlString("LINESTRING(47 1,46 1,46 0,47 0,47 1)")).MakeValidIfInvalid();
		SqlGeography multiLineString = SqlGeography.Parse(new SqlString("MULTILINESTRING((47.6415668949958 0.516357421875,47.34463879017405 0.516357421875, 47.22539733216678 0.977783203125,47.463611506072866 1.175537109375,47.6415668949958 0.516357421875),(47.86549372980948 0.764923095703125,47.82309640371982 0.951690673828125,47.79911736820551 1.220855712890625,47.69015026565801 1.089019775390625,47.656860648589 1.256561279296875))")).MakeValidIfInvalid();

		SqlGeography simplePoly = SqlGeography.Parse(new SqlString("POLYGON((47 1,46 1,46 0,47 0,47 1))")).MakeValidIfInvalid();
		SqlGeography polyWithHole = SqlGeography.Parse(new SqlString(@"POLYGON ((47.463611506072844 1.1755371093748681, 47.225397332166693 0.97778320312499889, 47.344638790174031 0.51635742187495515, 47.64156689499589 0.51635742187507339, 47.463611506072844 1.1755371093748681), (47.532762628989047 0.731964111328229, 47.549449624568226 0.6303405761719838, 47.493805649625926 0.63034057617193173, 47.482669772098561 0.729217529296964, 47.532762628989047 0.731964111328229))")).MakeValidIfInvalid();
		SqlGeography multiPolygon = SqlGeography.Parse(new SqlString(@"
					MULTIPOLYGON (
						((40 40, 45 20, 30 45, 40 40)),
						((35 20 , 20 45, 5 30, 10 10, 30 10,35 20), (20 30, 25 20, 15 20, 20 30)),
						((47.463611506072844 1.1755371093748681, 47.225397332166693 0.97778320312499889, 47.344638790174031 0.51635742187495515, 47.64156689499589 0.51635742187507339, 47.463611506072844 1.1755371093748681), (47.532762628989047 0.731964111328229, 47.549449624568226 0.6303405761719838, 47.493805649625926 0.63034057617193173, 47.482669772098561 0.729217529296964, 47.532762628989047 0.731964111328229))
					)")).MakeValidIfInvalid();

		SqlGeography geomCol = SqlGeography.Parse(new SqlString(@"
					GEOMETRYCOLLECTION (
						POLYGON((47.463611506072844 1.1755371093748681, 47.225397332166693 0.97778320312499889, 47.344638790174031 0.51635742187495515, 47.64156689499589 0.51635742187507339, 47.463611506072844 1.1755371093748681), (47.532762628989047 0.731964111328229, 47.549449624568226 0.6303405761719838, 47.493805649625926 0.63034057617193173, 47.482669772098561 0.729217529296964, 47.532762628989047 0.731964111328229)),
						LINESTRING(47.86549372980948 0.764923095703125,47.82309640371982 0.951690673828125,47.79911736820551 1.220855712890625,47.69015026565801 1.089019775390625,47.656860648589 1.256561279296875),
						POINT(47.817563762851776 0.767669677734375)
					)")).MakeValidIfInvalid();

		#endregion

	}
}
