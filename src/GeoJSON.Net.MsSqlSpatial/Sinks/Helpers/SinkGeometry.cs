using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial.Sinks
{
	internal class SinkGeometry<T> : List<SinkLineRing>
	{
		public T GeometryType { get; set; }

		public SinkGeometry(T geomType)
		{
			GeometryType = geomType;
		}
	}
}
