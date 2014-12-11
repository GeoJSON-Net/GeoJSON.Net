using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial.Sinks
{
	internal class SinkGeometryCollection : List<SinkGeometry>
	{
		public OpenGisGeometryType GeometryType { get; set; }

		public SinkGeometryCollection(OpenGisGeometryType geomType)
		{
			GeometryType = geomType;
		}
	}
}
