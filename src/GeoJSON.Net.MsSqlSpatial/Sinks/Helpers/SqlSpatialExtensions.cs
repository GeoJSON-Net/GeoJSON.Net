using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial
{
	public static class SqlSpatialExtensions
	{
		// Sql geometry extensions

		public static double[] BoundingBox(this SqlGeometry geom)
		{
			double xmin = Double.MaxValue, ymin = Double.MaxValue, xmax = Double.MinValue, ymax = double.MinValue;
			foreach (SqlGeometry point in geom.Points())
			{
				xmin = Math.Min(point.STX.Value, xmin);
				ymin = Math.Min(point.STY.Value, ymin);
				xmax = Math.Max(point.STX.Value, xmax);
				ymax = Math.Max(point.STY.Value, ymax);
			}
			return new double[] { xmin, ymin, xmax, ymax };
		}
		public static IEnumerable<SqlGeometry> Geometries(this SqlGeometry geom)
		{
			for (int i = 1; i <= geom.STNumGeometries().Value; i++)
			{
				yield return geom.STGeometryN(i);
			}
		}

		public static IEnumerable<SqlGeometry> Points(this SqlGeometry geom)
		{
			for (int i = 1; i <= geom.STNumPoints().Value; i++)
			{
				yield return geom.STPointN(i);
			}
		}

		public static IEnumerable<SqlGeometry> InteriorRings(this SqlGeometry geom)
		{
			for (int i = 1; i <= geom.STNumInteriorRing().Value; i++)
			{
				yield return geom.STInteriorRingN(i);
			}
		}

		public static SqlGeometry MakeValidIfInvalid(this SqlGeometry geom)
		{
			if (geom == null || geom.IsNull)
			{
				return geom;
			}

			// Make valid if necessary
			if (!geom.STIsValid().IsTrue)
			{
				return geom.MakeValid();
			}

			return geom;
		}


		// Sql geography extensions

		public static double[] BoundingBox(this SqlGeography geom)
		{
			double xmin = 180, ymin = 90, xmax = -180, ymax = -90;
			foreach (SqlGeography point in geom.Points())
			{
				xmin = Math.Min(point.Long.Value, xmin);
				ymin = Math.Min(point.Lat.Value, ymin);
				xmax = Math.Max(point.Long.Value, xmax);
				ymax = Math.Max(point.Lat.Value, ymax);
			}
			return new double[] { xmin, ymin, xmax, ymax };
		}
		public static IEnumerable<SqlGeography> Geometries(this SqlGeography geom)
		{
			for (int i = 1; i <= geom.STNumGeometries().Value; i++)
			{
				yield return geom.STGeometryN(i);
			}
		}

		public static IEnumerable<SqlGeography> Points(this SqlGeography geom)
		{
			for (int i = 1; i <= geom.STNumPoints().Value; i++)
			{
				yield return geom.STPointN(i);
			}
		}

		public static SqlGeography MakeValidIfInvalid(this SqlGeography geom)
		{
			if (geom == null || geom.IsNull)
			{
				return geom;
			}

			// Make valid if necessary
			if (!geom.STIsValid().IsTrue)
			{
				return geom.MakeValid();
			}

			return geom;
		}
	}
}
