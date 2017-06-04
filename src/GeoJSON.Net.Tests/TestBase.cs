using System;
using System.IO;
using System.Reflection;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        private static readonly string AssemblyName = typeof(TestBase)
            .GetTypeInfo().Assembly.GetName().Name;

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = typeof(TestBase).GetTypeInfo().Assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        protected string GetExpectedJson(string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Substring(AssemblyName.Length + 1);
            var path = Path.Combine(Path.Combine(AssemblyDirectory, projectFolder), $"{type}_{name}.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
    }
}