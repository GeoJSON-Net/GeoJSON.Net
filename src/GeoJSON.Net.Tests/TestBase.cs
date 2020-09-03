using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        private static readonly Assembly ThisAssembly = typeof(TestBase).GetTypeInfo().Assembly;
        private static readonly string AssemblyName = ThisAssembly.GetName().Name;
        public static readonly JsonSerializerOptions DefaultSerializerOptions = CreateSerializerOptions();

        private static JsonSerializerOptions CreateSerializerOptions() {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            options.Converters.Add(new JsonStringEnumConverter(new GeoJsonNamingPolicy()));

            return options;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = ThisAssembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Substring(AssemblyName.Length + 1);
            var path = Path.Combine(AssemblyDirectory, @"./", projectFolder, type + "_" + name + ".json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
    }
}
