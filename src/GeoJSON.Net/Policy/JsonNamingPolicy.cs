using System.Text.Json;
using System.Globalization;

namespace GeoJSON.Net
{
    public class GeoJsonNamingPolicy : JsonNamingPolicy {
        private TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public override string ConvertName(string name) => textInfo.ToTitleCase(name);
    }
}
