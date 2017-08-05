# TopoJSON.NET [![NuGet Version](http://img.shields.io/nuget/v/TopoJSON.NET.svg?style=flat)](https://www.nuget.org/packages/TopoJSON.NET/)

TopoJSON.Net is a fork of Jörg Battermann's wonderful GeoJSON.Net library. It extends the original lib with TopoJSON features and uses [Newtonsoft Json.NET](http://json.codeplex.com) converters for serialization and deserialization.

## What's in this version?
The current release features *only* deserialization (which might be buggy and I'll write more tests once I have the time). Serialization will come in the next weeks.

TopoJSON.Net aims to maintain full compatibility to GeoJSON.Net. You'll find classes prefixed with TopoJSON (such as `TopoJSONPolygon`) that work the same way as their GeoJSON.Net equivalents. Should be easy to exchange data between GeoJSON.Net and TopoJSON.Net.

## Wait! Can I see an example?

Yup.

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

## Installation & Usage
Well all you basically have to do is install the [TopoJSON.Net](https://www.nuget.org/packages/TopoJSON.Net/) NuGet package:

`PM> Install-Package TopoJSON.Net`

## Contributing
Highly welcome! Just fork away and send a pull request.


## Thanks
This library would be NOTHING without its [contributors](https://github.com/jbattermann/GeoJSON.Net/graphs/contributors) - thanks so much!!

## Copyright

GeoJSON.Net: Copyright © 2014 Jörg Battermann & [Contributors](https://github.com/jbattermann/GeoJSON.Net/graphs/contributors)
TopoJSON.Net: Copyright © 2015 Friedrich Politz

## License

GeoJSON and TopoJSON.Net are licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [LICENSE.md](https://github.com/Freddixx/TopoJSON.Net/blob/master/LICENSE.md) for more information.

