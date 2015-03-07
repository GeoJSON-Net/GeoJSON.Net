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
    using TopoJSON.Net.Geometry;

    [TestClass]
    public class TopoJSONSerializationTest
    {
        [TestMethod]
        public void TestGeneratedTopologySerialization() {
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

            Assert.AreNotEqual("", serializedJSON);
        }

        [TestMethod]
        public void TestRealStringTopologyDeserialization() {
            string topology_string = 
                /*              
                @"{
              'type':'Topology',
              'objects':{
                'fokus4711':{
                  'type':'Polygon',
                  'properties':{
                    'shorthand':'B',
                    'longhand':'Land Berlin',
                    'priority':0
                  },
                  'arcs':[[1]]
                }
              },
              'arcs':[[[52.13, 13.13], [53.13, 12.13]]],
              'bbox':[5.82275390625, 47.26432008025478, 15.073242187499998, 55.04061432771672],
              'properties':{
                'content_url':'https://example.com/katwarn_de/',
                'shorthand':'KW',
                'longhand':'KATWARN'
              }
            }"; */
            ///*
            @"
            {
            'type':'Topology',
            'transform':{
            'scale': [1,1],
            'translate': [0,0]
            },
            'objects':{
            'two-squares':{
                'type': 'GeometryCollection',
                'geometries':[
                {'type': 'Polygon', 'arcs':[[0,1]],'properties': {'name': 'Left_Polygon' }},
                {'type': 'Polygon', 'arcs':[[2,-1]],'properties': {'name': 'Right_Polygon' }}
                ]
            },
            'one-line': {
                'type':'GeometryCollection',
                'geometries':[
                {'type': 'LineString', 'arcs': [3],'properties':{'name':'Under_LineString'}}
                ]
            },
            'two-places':{
                'type':'GeometryCollection',
                'geometries':[
                {'type':'Point','coordinates':[0,0],'properties':{'name':'Origine_Point'}},
                {'type':'Point','coordinates':[0,-1],'properties':{'name':'Under_Point'}}
                ]
            }
            },
            'arcs': [
            [[1,2],[0,-2]],
            [[1,0],[-1,0],[0,2],[1,0]],
            [[1,2],[1,0],[0,-2],[-1,0]],
            [[0,-1],[2,0]]
            ]
            }
            ";
            //*/
            var topology = JsonConvert.DeserializeObject<Topology>(topology_string);
            Assert.IsInstanceOfType(topology, typeof(Topology));
            Assert.AreEqual(1, topology.Arcs.Count);
        }
    }
}
