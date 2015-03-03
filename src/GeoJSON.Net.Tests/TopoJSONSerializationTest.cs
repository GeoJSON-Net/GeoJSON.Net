using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Tests
{
    using GeoJSON.Net.Geometry;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Diagnostics;

    [TestClass]
    public class TopoJSONSerializationTest
    {
        [TestMethod]
        public void TestTopologySerialization() {
            Topology topology = new Topology();
            
            List<GeographicPosition> l1 = new List<GeographicPosition>();
            GeographicPosition ga1 = new GeographicPosition(52.0, 13.0);
            GeographicPosition ga2 = new GeographicPosition(52.1, 13.1);
            GeographicPosition ga3 = new GeographicPosition(52.2, 13.2);
            l1.Add(ga1);
            l1.Add(ga2);
            l1.Add(ga3);
            Arc arc1 = new Arc(l1);

            List<GeographicPosition> l2 = new List<GeographicPosition>();
            GeographicPosition ga4 = new GeographicPosition(8.1, 13.0);
            GeographicPosition ga5 = new GeographicPosition(8.3, 13.1);
            GeographicPosition ga6 = new GeographicPosition(8.5, 13.2);
            l2.Add(ga4);
            l2.Add(ga5);
            l2.Add(ga6);
            Arc arc2 = new Arc(l2);
            List<Arc> arcs = new List<Arc>();
            arcs.Add(arc1);
            arcs.Add(arc2);

            topology.Arcs = arcs;
            topology.BoundingBoxes = new double[]{ 52.0, 13.0, 8.0, 13.0 };

            var serializedJSON = JsonConvert.SerializeObject(
                topology, 
                Formatting.Indented, 
                new JsonSerializerSettings { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), 
                    NullValueHandling = NullValueHandling.Ignore 
                }
            );

            Debug.WriteLine(serializedJSON);
            Assert.AreNotEqual("", serializedJSON);
        }
    }
}
