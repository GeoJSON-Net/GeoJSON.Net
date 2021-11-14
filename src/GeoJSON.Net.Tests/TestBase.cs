using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        private static readonly Assembly ThisAssembly = typeof(TestBase)
        .Assembly;
        private static readonly string AssemblyName = ThisAssembly.GetName().Name;

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = ThisAssembly.Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = GetType().FullName;
            using (Stream stream = assembly.GetManifestResourceStream($"{type}_{name}.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }

            throw new ArgumentException("File with name could not be found");
        }
    }
}