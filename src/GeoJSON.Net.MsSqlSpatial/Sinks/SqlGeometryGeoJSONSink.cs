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
	///	// sink.BoundingBox returns a GeoJSON compliant double[] bbox 
	///	return sink.ConstructedGeometry; // returns an IGeometryObject
	///	
	///	</code> 
	/// </summary>
	internal class SqlGeometryGeoJsonSink : IGeometrySink110
	{
		SinkGeometryCollection<OpenGisGeometryType> _geomCollection;
		SinkGeometry<OpenGisGeometryType> _currentGeometry;
		SinkLineRing _currentRing;
		double _xmin = double.MaxValue;
		double _ymin = double.MaxValue;
		double _xmax = double.MinValue;
		double _ymax = double.MinValue;

		#region Sink implementation


		public void AddLine(double x, double y, double? z, double? m)
		{
			_currentRing.Add(new GeographicPosition(y, x, z));

			UpdateBoundingBox(x, y);
		}
		private void UpdateBoundingBox(double x, double y)
		{
			_xmin = Math.Min(x, _xmin);
			_ymin = Math.Min(y, _ymin);
			_xmax = Math.Max(x, _xmax);
			_ymax = Math.Max(y, _ymax);
		}

		public void BeginFigure(double x, double y, double? z, double? m)
		{
			_currentRing = new SinkLineRing();
			_currentRing.Add(new GeographicPosition(y, x, z));

			UpdateBoundingBox(x, y);
		}

		public void BeginGeometry(OpenGisGeometryType type)
		{
			if (_geomCollection == null)
			{
				_geomCollection = new SinkGeometryCollection<OpenGisGeometryType>(type);
			}

			_currentGeometry = new SinkGeometry<OpenGisGeometryType>(type);
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
						((Point)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					case OpenGisGeometryType.MultiPoint:
						_geometry = new MultiPoint(_geomCollection.Select(g => (Point)ConstructGeometryPart(g)).ToList());
						((MultiPoint)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					case OpenGisGeometryType.LineString:
						_geometry = ConstructGeometryPart(_geomCollection[0]);
						((LineString)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					case OpenGisGeometryType.MultiLineString:
						_geometry = new MultiLineString(_geomCollection.Select(g => (LineString)ConstructGeometryPart(g)).ToList());
						((MultiLineString)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					case OpenGisGeometryType.Polygon:
						_geometry = ConstructGeometryPart(_geomCollection.First());
						((Polygon)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					case OpenGisGeometryType.MultiPolygon:
						_geometry = new MultiPolygon(_geomCollection.Select(g => (Polygon)ConstructGeometryPart(g)).ToList()) { BoundingBoxes = this.BoundingBox };
						((MultiPolygon)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					case OpenGisGeometryType.GeometryCollection:
						_geometry = new GeometryCollection(_geomCollection.Select(g => ConstructGeometryPart(g)).ToList()) { BoundingBoxes = this.BoundingBox };
						((GeometryCollection)_geometry).BoundingBoxes = this.BoundingBox;
						break;
					default:
						throw new NotSupportedException("Geometry type " + _geomCollection.GeometryType.ToString() + " is not supported yet.");
				}

				return _geometry;
			}
		}

		public double[] BoundingBox
		{
			get
			{
				return new double[] { _xmin, _ymin, _xmax, _ymax };
			}
		}

		private IGeometryObject ConstructGeometryPart(SinkGeometry<OpenGisGeometryType> geomPart)
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
					throw new NotSupportedException("Geometry type " + geomPart.GeometryType.ToString() + " is not supported yet.");
			}

			return geometry;
		}


	}
}
