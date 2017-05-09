# GeoJSON.NET [![NuGet Version](http://img.shields.io/nuget/v/GeoJSON.NET.svg?style=flat)](https://www.nuget.org/packages/GeoJSON.NET/) [![Build status](https://ci.appveyor.com/api/projects/status/i5afnui06gqco0wi)](https://ci.appveyor.com/project/GeojsonNet/geojson-net)

GeoJSON.Net is a .NET library for the [RFC 7946 The GeoJSON Format](https://tools.ietf.org/html/rfc7946) and it uses and provides [Newtonsoft Json.NET](http://json.codeplex.com) converters for serialization and deserialization of GeoJSON data.


## Installation & Usage
Well all you basically have to do is install the [GeoJSON.Net](https://www.nuget.org/packages/GeoJSON.Net/) NuGet package:

`PM> Install-Package GeoJSON.Net`

To deserialize a json string:

`var geoJsonObject = JsonConvert.DeserializeObject<Point>(json);`

That's all there is. Really. From there on you can go ahead and (De-)Serialize GeoJSON using the provided [converters](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net/Converters) - see [the Tests for example usage](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net.Tests).

# Release Notes
## Version 1.0.1-alpha

Some big changes going into version 1. The code has been cleaned up a bit and the nuget package has had a couple of more targets added. Looking into the future .Net Core will be added as well.

- The class GeographicPosition has been deprecated and is on its way towards being totally removed in favour of the Position class to match the RFC
- More build targets added net35, net40, net45
- GeoJSON.Net.Feature namespace moved to GeoJSON.Net.Features to stop class ambiguity

From a code perspective

- Moved main project files to use new Roslyn style proj files with built in multi-targeting
- Comments all cleaned up
- New PowerShell build scripts (Credit should go to Newtonsoft as the inspiration for these) 


# News
Well it's probably best to check out the [commits](https://github.com/GeoJSON-Net/GeoJSON.Net/commits/master) what has been added over time.

# Contributing
Highly welcome! Just fork away and send a pull request.


# Thanks
This library would be NOTHING without its [contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors) - thanks so much!!

# Copyright

Copyright © 2017 Matt Hunt, Jörg Battermann & [Contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors)

# License

GeoJSON.Net is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [LICENSE.md](https://github.com/GeoJSON-Net/GeoJSON.Net/blob/master/LICENSE.md) for more information.

