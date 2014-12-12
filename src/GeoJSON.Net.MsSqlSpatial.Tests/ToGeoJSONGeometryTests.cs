using System;
using Microsoft.SqlServer.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlTypes;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.MsSqlSpatial.Tests
{
	[TestClass]
	public class ToGeoJSONGeometryTests
	{
		[TestMethod]
		public void ValidPointTest()
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
		public void ValidMultiPointTest()
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
		public void ValidLineStringTest()
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
		public void ValidMultiLineStringTest()
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
		public void ValidPolygonTest()
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
		public void ValidMultiPolygonTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiPolygon);
			var geoJSONobj = MsSqlSpatialConvert.ToGeoJSONObject<MultiPolygon>(multiPolygon);

			Assert.IsNotNull(geoJSON);
			Assert.IsNotNull(geoJSONobj);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiPolygon);
			Assert.AreEqual(geoJSONobj.Type, GeoJSONObjectType.MultiPolygon);
			CollectionAssert.AreEqual(geoJSONobj.BoundingBoxes, multiPolygon.BoundingBox());
			Assert.AreEqual(geoJSONobj.Coordinates.Count, 3);
			Assert.AreEqual(geoJSONobj.Coordinates[0].Coordinates.Count, 1);
			Assert.AreEqual(geoJSONobj.Coordinates[1].Coordinates.Count, 2);
			Assert.AreEqual(geoJSONobj.Coordinates[2].Coordinates.Count, 2);
		}

		[TestMethod]
		public void ValidGeometryCollectionTest()
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

		#region Test geometries

		SqlGeometry simplePoint = SqlGeometry.Point(1, 47, 4326);
		SqlGeometry multiPoint = SqlGeometry.Parse(new SqlString("MULTIPOINT((1 47),(1 46),(0 46),(0 47),(1 47))"));
		SqlGeometry lineString = SqlGeometry.Parse(new SqlString("LINESTRING(1 47,1 46,0 46,0 47,1 47)"));
		SqlGeometry multiLineString = SqlGeometry.Parse(new SqlString("MULTILINESTRING((0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),(0.764923095703125 47.86549372980948,0.951690673828125 47.82309640371982,1.220855712890625 47.79911736820551,1.089019775390625 47.69015026565801,1.256561279296875 47.656860648589))"));

		SqlGeometry simplePoly = SqlGeometry.Parse(new SqlString("POLYGON((1 47,1 46,0 46,0 47,1 47))"));
		SqlGeometry polyWithHole = SqlGeometry.Parse(new SqlString(@"
					POLYGON(
					(0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),
					(0.630340576171875 47.54944962456812,0.630340576171875 47.49380564962583,0.729217529296875 47.482669772098674,0.731964111328125 47.53276262898896,0.630340576171875 47.54944962456812)
					)"));
		SqlGeometry multiPolygon = SqlGeometry.Parse(new SqlString(@"
					MULTIPOLYGON (
						((40 40, 20 45, 45 30, 40 40)),
						((20 35, 45 20, 30 5, 10 10, 10 30, 20 35), (30 20, 20 25, 20 15, 30 20)),
						((0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),(0.630340576171875 47.54944962456812,0.630340576171875 47.49380564962583,0.729217529296875 47.482669772098674,0.731964111328125 47.53276262898896,0.630340576171875 47.54944962456812))
					)"));

		SqlGeometry geomCol = SqlGeometry.Parse(new SqlString(@"
					GEOMETRYCOLLECTION (
						POLYGON((0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),(0.630340576171875 47.54944962456812,0.630340576171875 47.49380564962583,0.729217529296875 47.482669772098674,0.731964111328125 47.53276262898896,0.630340576171875 47.54944962456812)),
						LINESTRING(0.764923095703125 47.86549372980948,0.951690673828125 47.82309640371982,1.220855712890625 47.79911736820551,1.089019775390625 47.69015026565801,1.256561279296875 47.656860648589),
						POINT(0.767669677734375 47.817563762851776)
					)"));

		#endregion

	}
}
