using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;

namespace GeoJSON.Net.MsSqlSpatial.Sinks
{
	/// <summary>
	/// Sink converting a SqlGeometry to a GeoJSON geometry
	/// Usage : <code>SqlGeometryGeoJsonSink sink = new SqlGeometryGeoJsonSink();
	///	sqlGeometry.Populate(sink);
	///	return sink.ConstructedGeometry; // returns an IGeometryObject
	///	</code> 
	///	<remarks>WARN: this sink assumes that geometries X are longitudes and Y are latitudes
	///	TODO: bbox calculations while accumulating coordinates</remarks>
	/// </summary>
	internal class SqlGeometryGeoJsonSink : IGeometrySink110
	{
		private SinkGeometryCollection _geomCollection;
		private SinkGeometry _currentGeometry;
		private SinkLineRing _currentRing;

		#region Sink implementation

		public void AddLine(double x, double y, double? z, double? m)
		{
			_currentRing.Add(new GeographicPosition(y, x, z));
		}

		public void BeginFigure(double x, double y, double? z, double? m)
		{
			_currentRing = new SinkLineRing();
			_currentRing.Add(new GeographicPosition(y, x, z));
		}

		public void BeginGeometry(OpenGisGeometryType type)
		{
			if (_geomCollection == null)
			{
				_geomCollection = new SinkGeometryCollection(type);
			}

			_currentGeometry = new SinkGeometry(type);
		}

		public void EndFigure()
		{
			if (_currentRing == null)
				return;

			_currentGeometry.Add(_currentRing);
			_currentRing = null;
		}

		public void EndGeometry()
		{
			if (_currentGeometry == null)
				return;

			_geomCollection.Add(_currentGeometry);
			_currentGeometry = null;
		}

		public void SetSrid(int srid)
		{

		}

		// Not implemented
		// This one is tough ! Implementation should use SqlGeometry.CurveToLineWithTolerance
		public void AddCircularArc(double x1, double y1, double? z1, double? m1, double x2, double y2, double? z2, double? m2)
		{
			throw new NotImplementedException();
		}

		#endregion

		public IGeometryObject ConstructedGeometry
		{
			get
			{
				IGeometryObject _geometry = null;

				switch (_geomCollection.GeometryType)
				{
					case OpenGisGeometryType.Point:
						_geometry = ConstructGeometryPart(_geomCollection[0]);
						break;
					case OpenGisGeometryType.MultiPoint:
						_geometry = new MultiPoint(_geomCollection.Select(g => (Point)ConstructGeometryPart(g)).ToList());
						break;
					case OpenGisGeometryType.LineString:
						_geometry = ConstructGeometryPart(_geomCollection[0]);
						break;
					case OpenGisGeometryType.MultiLineString:
						_geometry = new MultiLineString(_geomCollection.Select(g => (LineString)ConstructGeometryPart(g)).ToList());
						break;
					case OpenGisGeometryType.Polygon:
						_geometry = ConstructGeometryPart(_geomCollection.First());
						break;
					case OpenGisGeometryType.MultiPolygon:
						_geometry = new MultiPolygon(_geomCollection.Select(g => (Polygon)ConstructGeometryPart(g)).ToList());
						break;
					case OpenGisGeometryType.GeometryCollection:
						_geometry = new GeometryCollection(_geomCollection.Select(g => ConstructGeometryPart(g)).ToList());
						break;
					default:
						throw new NotImplementedException("Geometry type " + _geomCollection.GeometryType.ToString() + " is not implemented.");
				}

				return _geometry;
			}
		}

		private IGeometryObject ConstructGeometryPart(SinkGeometry geomPart)
		{

			IGeometryObject geometry = null;

			switch (geomPart.GeometryType)
			{
				case OpenGisGeometryType.Point:
					geometry = new Point(geomPart[0][0]);
					break;
				case OpenGisGeometryType.MultiPoint:
					MultiPoint mp = new MultiPoint(geomPart.Select(g => new Point(g[0])).ToList());
					geometry = mp;
					break;
				case OpenGisGeometryType.LineString:
					geometry = new LineString(geomPart[0]);
					break;
				case OpenGisGeometryType.MultiLineString:
					geometry = new MultiLineString(geomPart.Select(line => new LineString(line))
																																		.ToList()
																															);
					break;
				case OpenGisGeometryType.Polygon:
					geometry = new Polygon(geomPart.Select(line => new LineString(line))
																																		.ToList()
																															);
					break;

				default:
					throw new NotImplementedException("Geometry type " + geomPart.GeometryType.ToString() + " is not implemented.");
			}

			return geometry;
		}


	}
}
