using System;
using Microsoft.SqlServer.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlTypes;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.MsSqlSpatial.Tests
{
	[TestClass]
	public class ToGeoJSONTests
	{
		[TestMethod]
		public void ValidPointTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(simplePoint);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.Point);
		}

		[TestMethod]
		public void ValidMultiPointTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiPoint);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiPoint);
		}

		[TestMethod]
		public void ValidLineStringTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(lineString);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.LineString);
		}

		[TestMethod]
		public void ValidMultiLineStringTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiLineString);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiLineString);
		}

		[TestMethod]
		public void ValidPolygonTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(simplePoly);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.Polygon);


			geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(polyWithHole);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.Polygon);
			Polygon geoJSONPolygon = (Polygon)geoJSON;
			Assert.AreEqual(geoJSONPolygon.Coordinates.Count, 2);
		}

		[TestMethod]
		public void ValidMultiPolygonTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(multiPolygon);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.MultiPolygon);
			MultiPolygon geoJSONtyped = (MultiPolygon)geoJSON;
			Assert.AreEqual(geoJSONtyped.Coordinates.Count, 3);
			Assert.AreEqual(geoJSONtyped.Coordinates[0].Coordinates.Count, 1);
			Assert.AreEqual(geoJSONtyped.Coordinates[1].Coordinates.Count, 2);
			Assert.AreEqual(geoJSONtyped.Coordinates[2].Coordinates.Count, 2);
		}

		[TestMethod]
		public void ValidGeometryCollectionTest()
		{
			IGeometryObject geoJSON = MsSqlSpatialConvert.ToGeoJSONGeometry(geomCol);

			Assert.IsNotNull(geoJSON);
			Assert.AreEqual(geoJSON.Type, GeoJSONObjectType.GeometryCollection);

			GeometryCollection geoJSONtyped = (GeometryCollection)geoJSON;
			Assert.AreEqual(geoJSONtyped.Geometries.Count, 3);
			Assert.AreEqual(geoJSONtyped.Geometries[0].Type, GeoJSONObjectType.Polygon);
			Assert.AreEqual(geoJSONtyped.Geometries[1].Type, GeoJSONObjectType.LineString);
			Assert.AreEqual(geoJSONtyped.Geometries[2].Type, GeoJSONObjectType.Point);
		}

		//[TestMethod]
		//public void FeatureTest()
		//{

		//	//var simplePointClass = new { name = "Simple point", doubleProperty = 1.2345d, geomProp = simplePoint };
		//	//var multiPointClass = new { name = "Multi point", prop2 = 1.2345d, geomProp = multiPoint };
		//	//var lineStringClass = new { name = "Line string", prop2 = 1.2345d, geomProp = lineString };
		//	//var multiLineStringClass = new { name = "Multi line string", prop2 = 1.2345d, geomProp = multiLineString };
		//	//var simplePolyClass = new { name = "Simple polygon", prop2 = 1.2345d, geomProp = simplePoly };
		//	//var polyWithHoleClass = new { name = "Polygon with hole", prop2 = 1.2345d, geomProp = polyWithHole };
		//	//var multiPolygonClass = new { name = "Multipolygon", prop2 = 1.2345d, geomProp = multiPolygon };
		//	//var geomColClass = new { name = "Geometry collection (polygon+line+point)", prop2 = 1.2345d, geomProp = geomCol };

		//	//Feature simplePointFeature = simplePointClass.ToGeoJsonFeature();
		//	//Feature multiPointFeature = multiPointClass.ToGeoJsonFeature();
		//	//Feature lineStringFeature = lineStringClass.ToGeoJsonFeature();
		//	//Feature multiLineStringFeature = multiLineStringClass.ToGeoJsonFeature();
		//	//Feature simplePolyFeature = simplePolyClass.ToGeoJsonFeature();
		//	//Feature polyFeature = polyWithHoleClass.ToGeoJsonFeature();
		//	//Feature multiPolygonFeature = multiPolygonClass.ToGeoJsonFeature();
		//	//Feature geomColFeature = geomColClass.ToGeoJsonFeature();
		//}

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

	}
}
