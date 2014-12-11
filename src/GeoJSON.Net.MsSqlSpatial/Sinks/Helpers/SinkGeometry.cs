using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial.Sinks
{
	internal class SinkGeometry : List<SinkLineRing>
	{
		public OpenGisGeometryType GeometryType { get; set; }

		public SinkGeometry(OpenGisGeometryType geomType)
		{
			GeometryType = geomType;
		}
	}
}
