# GeoJSON.NET
[![NuGet Version](http://img.shields.io/nuget/v/GeoJSON.NET.svg?style=flat)](https://www.nuget.org/packages/GeoJSON.NET/) 

[![Build status](https://ci.appveyor.com/api/projects/status/n4q1opb6dod0hwac?svg=true)](https://ci.appveyor.com/project/matt-lethargic/geojson-net)

GeoJSON.Net is a .NET library for the [RFC 7946 The GeoJSON Format](https://tools.ietf.org/html/rfc7946) and it uses and provides [Newtonsoft Json.NET](http://json.codeplex.com) converters for serialization and deserialization of GeoJSON data.


## Installation & Usage
Well all you basically have to do is install the [GeoJSON.Net](https://www.nuget.org/packages/GeoJSON.Net/) NuGet package:

`PM> Install-Package GeoJSON.Net`

To deserialize a json string:

`var geoJsonObject = JsonConvert.DeserializeObject<Point>(json);`

That's all there is. Really. From there on you can go ahead and (De-)Serialize GeoJSON using the provided [converters](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net/Converters) - see [the Tests for example usage](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net.Tests).


## News
It's probably best to check out the [commits](https://github.com/GeoJSON-Net/GeoJSON.Net/commits/master) and the [issues](https://github.com/GeoJSON-Net/GeoJSON.Net/issues) what has been added over time.

## Contributing
Highly welcome! Just fork away and send a pull request.


## Thanks
This library would be NOTHING without its [contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors) - thanks so much!!

## Copyright

Copyright © 2017 Jörg Battermann & [Contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors)

## License

GeoJSON.Net is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [LICENSE.md](https://github.com/GeoJSON-Net/GeoJSON.Net/blob/master/LICENSE.md) for more information.

