using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoJSON.Net.Geometry;

namespace GeoJSON.Net.Tests
{
    [TestClass]
    public class TopoJSONDeserializationTest
    {

        [TestMethod]
        public void TestComplexTopologyDeserialization()
        {
            string topology_string =
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
            var topology = JsonConvert.DeserializeObject<Topology>(topology_string);
            Assert.IsTrue(topology.Objects.Exists(o => o.Name == "two-squares"));
            Assert.IsTrue(topology.Objects.Exists(o => o.Name == "one-line"));
            Assert.IsTrue(topology.Objects.Exists(o => o.Name == "two-places"));
            Assert.IsInstanceOfType(topology, typeof(Topology));
            Assert.AreEqual(4, topology.Arcs.Count);
            Assert.AreEqual(3, topology.Objects.Count);
        }

        [TestMethod]
        public void TestShortTopologyDeserialization()
        {
            string topology_string =
            @"{
              'type':'Topology',
              'objects':{
                'estate412':{
                  'type':'Polygon',
                  'properties':{
                    'property1':'Test',
                    'property2':'Germany',
                    'priority':0
                  },
                  'arcs':[[0]]
                }
              },
              'arcs':[[[52.13, 13.13], [53.13, 12.13]]],
              'bbox':[5.82275390625, 47.26432008025478, 15.073242187499998, 55.04061432771672],
              'properties':{
                'content_url':'https://example.com/',
                'short':'GER',
                'long':'Germany'
              }
            }";
            var topology = JsonConvert.DeserializeObject<Topology>(topology_string);
            Assert.IsInstanceOfType(topology, typeof(Topology));
            Assert.AreEqual(1, topology.Arcs.Count);
            Assert.AreEqual(1, topology.Objects.Count);
        }


        [TestMethod]
        public void TestStandardTopologyDeserialization()
        {
            string topology_string =
            @"{
              'type': 'Topology',
              'objects': {
                'example': {
                  'type': 'GeometryCollection',
                  'geometries': [
                    {
                      'type': 'Point',
                      'properties': {
                        'prop0': 'value0'
                      },
                      'coordinates': [102, 0.5]
                    },
                    {
                      'type': 'LineString',
                      'properties': {
                        'prop0': 'value0',
                        'prop1': 0
                      },
                      'arcs': [0]
                    },
                    {
                      'type': 'Polygon',
                      'properties': {
                        'prop0': 'value0',
                        'prop1': {
                          'this': 'that'
                        }
                      },
                      'arcs': [[-2]]
                    }
                  ]
                }
              },
              'arcs': [
                [[102, 0], [103, 1], [104, 0], [105, 1]],
                [[100, 0], [101, 0], [101, 1], [100, 1], [100, 0]]
              ]
            }
            ";
            var topology = JsonConvert.DeserializeObject<Topology>(topology_string);
            Assert.IsTrue(topology.Objects.Exists(o => o.Name == "example"));
            Assert.AreEqual(2, topology.Arcs.Count);
        }


        [TestMethod]
        public void TestQuantizedTopologyDeserialization()
        {
            string topology_string =
            @"{
             'type': 'Topology',
              'transform': {
                'scale': [0.0005000500050005, 0.00010001000100010001],
                'translate': [100, 0]
              },
              'objects': {
                'example': {
                  'type': 'GeometryCollection',
                  'geometries': [
                    {
                      'type': 'Point',
                      'properties': {
                        'prop0': 'value0'
                      },
                      'coordinates': [4000, 5000]
                    },
                    {
                      'type': 'LineString',
                      'properties': {
                        'prop0': 'value0',
                        'prop1': 0
                      },
                      'arcs': [0]
                    },
                    {
                      'type': 'Polygon',
                      'properties': {
                        'prop0': 'value0',
                        'prop1': {
                          'this': 'that'
                        }
                      },
                      'arcs': [[1]]
                    }
                  ]
                }
              },
              'arcs': [
                [[4000, 0], [1999, 9999], [2000, -9999], [2000, 9999]],
                [[0, 0], [0, 9999], [2000, 0], [0, -9999], [-2000, 0]]
              ]
            }
            ";
            var topology = JsonConvert.DeserializeObject<Topology>(topology_string);
            Assert.IsTrue(topology.Objects.Exists(o => o.Name == "example"));
            Assert.AreEqual(2, topology.Arcs.Count);
            Assert.AreEqual(100, topology.Transform.Translation[0]);
            Assert.AreEqual(0, topology.Transform.Translation[1]);
        }
    }
}