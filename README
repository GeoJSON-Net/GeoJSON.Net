##What is GeoJSON.NET
GeoJSON is .NET library for the GeoJSON spec v1.0 (see http://geojson.org/geojson-spec.html), it uses Newtonsoft Json.NET (http://json.codeplex.com) for serialization and deserialization.

##Installing
1. Available as NuGet package via 'Install-Package GeoJSON.Net', see https://nuget.org/packages/GeoJSON.Net
2. Download the source, compile and include GeoJSON.Net.dll in you project

##Examples
### Deserialize GeoJSON file
The example shows how to deserialize GeoJSON file:
    ```csharp
using (FileStream fs = System.IO.File.Open(pathToFile, FileMode.Open))
{
    var fileContent = new StreamReader(fs).ReadToEnd();
    var features = JsonConvert.DeserializeObject<GeoJSON.Net.Feature.FeatureCollection>(fileContent);
}```



Enjoy!
-Joerg Battermann
jb at joergbattermann.com