using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        private static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Substring(AssemblyName.Length + 1);
            var path = Path.Combine(@".\", projectFolder, type + "_" + name + ".json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
    }
}