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
			if (!sqlGeometry.STIsValid().IsTrue)
			{
				sqlGeometry = sqlGeometry.MakeValid();

				// If still invalid, throw an exception
				if (sqlGeometry.STIsValid().IsFalse)
				{
					throw new Exception("Invalid geometry : " + sqlGeometry.IsValidDetailed());
				}
			}

			// Conversion using geometry sink
			SqlGeometryGeoJsonSink sink = new SqlGeometryGeoJsonSink();
			sqlGeometry.Populate(sink);
			return sink.ConstructedGeometry;
		}

		public static SqlGeometry ToSqlGeometry(IGeometryObject geoJsonGeometry)
		{
			throw new NotImplementedException();
			//switch(geoJsonGeometry.Type)
			//{
			//	case GeoJSONObjectType.
			//}
		}
	}
}
