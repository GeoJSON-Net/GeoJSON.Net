using GeoJSON.Net.Converters;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace GeoJSON.Net.Tests
{
    public class MiscTest
    {
        /// <summary>
        /// Test that the last coordinate must be the same as the first to complete the polygon
        /// </summary>
        [Fact]
        public void LineStringIsClosed() 
        {
            var coordinates = new List<GeographicPosition> 
            { 
                new GeographicPosition(52.370725881211314, 4.889259338378906), 
                new GeographicPosition(52.3711451105601, 4.895267486572266), 
                new GeographicPosition(52.36931095278263, 4.892091751098633), 
                new GeographicPosition(52.370725881211314, 4.889259338378906) 
            }.ToList<IPosition>();

            var lineString = new LineString(coordinates);
        }
    }
}
