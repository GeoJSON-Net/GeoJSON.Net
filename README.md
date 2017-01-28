#GeoJSON.NET [![NuGet Version](http://img.shields.io/nuget/v/GeoJSON.NET.svg?style=flat)](https://www.nuget.org/packages/GeoJSON.NET/) [![Build status](https://ci.appveyor.com/api/projects/status/lfxlj13sa5vk0l3y)](https://ci.appveyor.com/project/jbattermann/geojson-net)

GeoJSON.Net is a .NET library for the [GeoJSON v1.0 specificaton](http://geojson.org/geojson-spec.html) and it uses and provides [Newtonsoft Json.NET](http://json.codeplex.com) converters for serialization and deserialization of GeoJSON data.

##!! Project is no longer maintained / Up for grabs !!
This project has started a long time ago because I needed something like it for a personal project back then but it never got much love and effort from me over, mostly because I ended / finished said personal project and never had any use for GeoJSON.Net myself. My own bits of code in it reflect the early stages back then and it would require some major overhaul if not re-write to be considered a lean, maintainable library. It never got past a v0.1.x and that was for a / several good reasons.

As I cannot and want not devote any more personal, spare time on the project and would not want any users to think that it *is* actively under development / maintenance, I'd either declare it dead and remove it from NuGet or, if someone steps up, hand it over to one or multiple maintainers that might take better care of it.

If you're interested and do intend to take better care of the project get in touch in the corresponding [issue #68](https://github.com/GeoJSON-Net/GeoJSON.Net/issues/68).


##Installation & Usage
Well all you basically have to do is install the [GeoJSON.Net](https://www.nuget.org/packages/GeoJSON.Net/) NuGet package:

`PM> Install-Package GeoJSON.Net`

That's all there is. Really. From there on you can go ahead and (De-)Serialize GeoJSON using the provided [converters](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net/Converters) - see [the Tests for example usage](https://github.com/GeoJSON-Net/GeoJSON.Net/tree/master/src/GeoJSON.Net.Tests).


##News
Well it's probably best to check out the [commits](https://github.com/GeoJSON-Net/GeoJSON.Net/commits/master) what has been added over time.

## Contributing
Highly welcome! Just fork away and send a pull request.


##Thanks
This library would be NOTHING without its [contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors) - thanks so much!!

## Copyright

Copyright © 2017 Jörg Battermann & [Contributors](https://github.com/GeoJSON-Net/GeoJSON.Net/graphs/contributors)

## License

GeoJSON.Net is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [LICENSE.md](https://github.com/GeoJSON-Net/GeoJSON.Net/blob/master/LICENSE.md) for more information.

