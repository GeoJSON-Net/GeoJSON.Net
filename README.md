[![Backers on Open Collective](https://opencollective.com/geojson-net/backers/badge.svg)](#backers) [![Sponsors on Open Collective](https://opencollective.com/geojson-net/sponsors/badge.svg)](#sponsors) [![NuGet Version](http://img.shields.io/nuget/v/GeoJSON.NET.svg?style=flat)](https://www.nuget.org/packages/GeoJSON.NET/) 
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

## Contributors

This project exists thanks to all the people who contribute. 
<a href="https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors"><img src="https://opencollective.com/geojson-net/contributors.svg?width=890&button=false" /></a>


## Backers

Thank you to all our backers! 🙏 [[Become a backer](https://opencollective.com/geojson-net#backer)]

<a href="https://opencollective.com/geojson-net#backers" target="_blank"><img src="https://opencollective.com/geojson-net/backers.svg?width=890"></a>


## Sponsors

Support this project by becoming a sponsor. Your logo will show up here with a link to your website. [[Become a sponsor](https://opencollective.com/geojson-net#sponsor)]

<a href="https://opencollective.com/geojson-net/sponsor/0/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/0/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/1/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/1/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/2/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/2/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/3/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/3/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/4/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/4/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/5/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/5/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/6/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/6/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/7/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/7/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/8/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/8/avatar.svg"></a>
<a href="https://opencollective.com/geojson-net/sponsor/9/website" target="_blank"><img src="https://opencollective.com/geojson-net/sponsor/9/avatar.svg"></a>


