using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        public static readonly JsonSerializerSettings DefaultJsonSerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
    }
}