using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial.Sinks
{
	internal class SinkGeometryCollection<T> : List<SinkGeometry<T>>
	{
		public T GeometryType { get; set; }

		public SinkGeometryCollection(T geomType)
		{
			GeometryType = geomType;
		}
	}
}
