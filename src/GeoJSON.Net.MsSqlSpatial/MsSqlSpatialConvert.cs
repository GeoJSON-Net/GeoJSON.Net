using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.MsSqlSpatial.Sinks;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial
{
	public static class MsSqlSpatialConvert
	{
		/// <summary>
		/// Converts a native Sql Server geometry (lat/lon) to GeoJSON geometry
		/// </summary>
		/// <param name="sqlGeometry">SQL Server geometry to convert</param>
		/// <returns>GeoJSON geometry</returns>
		public static IGeometryObject ToGeoJSONGeometry(SqlGeometry sqlGeometry)
		{
			if (sqlGeometry == null || sqlGeometry.IsNull)
			{
				return null;
			}

			// Make valid if necessary
			sqlGeometry = sqlGeometry.MakeValidIfInvalid();
			if (sqlGeometry.STIsValid().IsFalse)
			{
				throw new Exception("Invalid geometry : " + sqlGeometry.IsValidDetailed());
			}

			// Conversion using geometry sink
			SqlGeometryGeoJsonSink sink = new SqlGeometryGeoJsonSink();
			sqlGeometry.Populate(sink);
			return sink.ConstructedGeometry;
		}

		/// <summary>
		/// Converts a native Sql Server geometry (lat/lon) to GeoJSON geometry
		/// </summary>
		/// <param name="sqlGeometry">SQL Server geometry to convert</param>
		/// <returns>GeoJSON geometry</returns>
		public static T ToGeoJSONObject<T>(SqlGeometry sqlGeometry) where T : GeoJSONObject
		{
			if (sqlGeometry == null || sqlGeometry.IsNull)
			{
				return null;
			}

			// Make valid if necessary
			sqlGeometry = sqlGeometry.MakeValidIfInvalid();
			if (sqlGeometry.STIsValid().IsFalse)
			{
				throw new Exception("Invalid geometry : " + sqlGeometry.IsValidDetailed());
			}

			// Conversion using geometry sink
			T geoJSONobj = null;
			SqlGeometryGeoJsonSink sink = new SqlGeometryGeoJsonSink();
			sqlGeometry.Populate(sink);
			geoJSONobj = sink.ConstructedGeometry as T;
			geoJSONobj.BoundingBoxes = sink.BoundingBox;

			return geoJSONobj;
		}

		public static SqlGeometry ToSqlGeometry(IGeometryObject geoJsonGeometry)
		{
			throw new NotImplementedException();
			//switch(geoJsonGeometry.Type)
			//{
			//	case GeoJSONObjectType.
			//}
		}



		/// <summary>
		/// Converts a native Sql Server geography to GeoJSON geometry
		/// </summary>
		/// <param name="sqlGeography">SQL Server geography to convert</param>
		/// <returns>GeoJSON geometry</returns>
		public static IGeometryObject ToGeoJSONGeometry(SqlGeography sqlGeography)
		{
			if (sqlGeography == null || sqlGeography.IsNull)
			{
				return null;
			}

			// Make valid if necessary
			sqlGeography = sqlGeography.MakeValidIfInvalid();
			if (sqlGeography.STIsValid().IsFalse)
			{
				throw new Exception("Invalid geometry : " + sqlGeography.IsValidDetailed());
			}

			// Conversion using geography sink
			SqlGeographyGeoJsonSink sink = new SqlGeographyGeoJsonSink();
			sqlGeography.Populate(sink);
			return sink.ConstructedGeography;
		}

		/// <summary>
		/// Converts a native Sql Server geography to GeoJSON geometry
		/// </summary>
		/// <param name="sqlGeography">SQL Server geography to convert</param>
		/// <returns>GeoJSON geometry</returns>
		public static T ToGeoJSONObject<T>(SqlGeography sqlGeography) where T : GeoJSONObject
		{
			if (sqlGeography == null || sqlGeography.IsNull)
			{
				return null;
			}

			// Make valid if necessary
			sqlGeography = sqlGeography.MakeValidIfInvalid();
			if (sqlGeography.STIsValid().IsFalse)
			{
				throw new Exception("Invalid geometry : " + sqlGeography.IsValidDetailed());
			}

			// Conversion using geography sink
			T geoJSONobj = null;
			SqlGeographyGeoJsonSink sink = new SqlGeographyGeoJsonSink();
			sqlGeography.Populate(sink);
			geoJSONobj = sink.ConstructedGeography as T;
			geoJSONobj.BoundingBoxes = sink.BoundingBox;

			return geoJSONobj;
		}

		public static SqlGeography ToSqlGeography(IGeometryObject geoJsonGeometry)
		{
			throw new NotImplementedException();
			//switch(geoJsonGeometry.Type)
			//{
			//	case GeoJSONObjectType.
			//}
		}
	}
}
