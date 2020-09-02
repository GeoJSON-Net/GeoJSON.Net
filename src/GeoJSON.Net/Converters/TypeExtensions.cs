using System;
using System.Reflection;

namespace GeoJSON.Net.Converters
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Encapsulates the framework-dependent preprocessor guards.
        /// </summary>
        public static bool IsAssignableFromType(this Type self, Type other)
        {
            return self.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
        }

    }
}
