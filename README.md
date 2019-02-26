[![NuGet Version](http://img.shields.io/nuget/v/GeoJSON.NET.svg?style=flat)](https://www.nuget.org/packages/GeoJSON.NET/) 
[![Build status](https://ci.appveyor.com/api/projects/status/i5afnui06gqco0wi/branch/master?svg=true)](https://ci.appveyor.com/project/GeojsonNet/geojson-net/branch/master)

# GeoJSON.NET
GeoJSON.Net is a .NET library for the [RFC 7946 The GeoJSON Format](https://tools.ietf.org/html/rfc7946) and it uses and provides [Newtonsoft Json.NET](http://json.codeplex.com) converters for serialization and deserialization of GeoJSON data.

## Version 2
I'm starting to put together a plan for version 2 of GeoJSON.Net. I'm open to any suggestions or ideas, if you have any thoughts please open an issue and make it clear that it's an idea for version to and I'll tag it up as such.

## Installation & Usage

[GeoJSON.Net NuGet package](https://www.nuget.org/packages/GeoJSON.Net/):
`Install-Package GeoJSON.Net`

### Serialization

```csharp
Position position = new Position(51.899523, -2.124156);
Point point = new Point(position);

string json = JsonConvert.SerializeObject(point);
```

### Deserialization

```csharp
string json = "{\"coordinates\":[-2.124156,51.899523],\"type\":\"Point\"}";

Point point = JsonConvert.DeserializeObject<Point>(json);
```

See the [Tests](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net.Tests) for more examples.


## Contributing
Highly welcome! Just fork away and send a pull request. We try and review most pull requests within a couple of days. There is now a version 2.0.0 branch. I've created this ready for any breaking changes and any extra features and would encourage anything that isn't a bug fix to go in there.

## Thanks
This library would be NOTHING without its [contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors) - thanks so much!!

## Sponsors

 We have the awesome .NET tools from [JetBrains](http://www.jetbrains.com/).

[![Resharper](http://www.filehelpers.net/images/tools_resharper.gif)](http://www.jetbrains.com/resharper/)
