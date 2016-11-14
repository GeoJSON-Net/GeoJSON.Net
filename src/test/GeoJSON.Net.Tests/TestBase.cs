using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        private static readonly string AssemblyName = typeof(TestBase).Name;

        public static string AssemblyDirectory
        {
            get
            {              
                var codeBase = System.AppContext.BaseDirectory;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return path;
                
            }
        }

        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Split('.').Last();
            var path = Path.Combine(AssemblyDirectory, @".\", projectFolder, type + "_" + name + ".json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
    }
}