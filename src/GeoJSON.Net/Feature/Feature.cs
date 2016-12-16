// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Feature type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;

namespace GeoJSON.Net.Feature
{
    /// <summary>
    ///     A GeoJSON <see cref="http://geojson.org/geojson-spec.html#feature-objects">Feature Object</see>.
    /// </summary>
    public class Feature : GeoJSONObject, IEqualityComparer<Feature>, IEquatable<Feature>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="id">The (optional) identifier.</param>
        [JsonConstructor]
        public Feature(IGeometryObject geometry, Dictionary<string, object> properties = null, string id = null)
        {
            Geometry = geometry;
            Properties = properties ?? new Dictionary<string, object>();
            Id = id;

            Type = GeoJSONObjectType.Feature;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">
        ///     Class used to fill feature properties. Any public member will be added to feature
        ///     properties
        /// </param>
        /// <param name="id">The (optional) identifier.</param>
        public Feature(IGeometryObject geometry, object properties, string id = null)
        {
            Geometry = geometry;
            Id = id;

            if (properties == null)
            {
                Properties = new Dictionary<string, object>();
            }
            else
            {
                Properties = properties.GetType().GetTypeInfo().DeclaredProperties
                    .Where(propertyInfo => propertyInfo.GetMethod.IsPublic)
                    .ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(properties, null));
            }

            Type = GeoJSONObjectType.Feature;
        }

        /// <summary>
        ///     Gets or sets the geometry.
        /// </summary>
        /// <value>
        ///     The geometry.
        /// </value>
        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public IGeometryObject Geometry { get; private set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The handle.</value>
        [JsonProperty(PropertyName = "id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        ///     Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [JsonProperty(PropertyName = "properties", Required = Required.AllowNull)]
        public Dictionary<string, object> Properties { get; private set; }

        #region IEqualityComparer, IEquatable

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(this, obj as Feature);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public bool Equals(Feature other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public bool Equals(Feature left, Feature right)
        {
            if (base.Equals(left, right))
            {
                return GetHashCode(left) == GetHashCode(right); 
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator ==(Feature left, Feature right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            if (ReferenceEquals(null, right))
            {
                return false;
            }
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified object instances are not considered equal
        /// </summary>
        public static bool operator !=(Feature left, Feature right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (Geometry != null)
            {
                hash = (hash * 397) ^ Geometry.GetHashCode();
            }
            if (Properties != null && Properties.Count > 0)
            {
                hash = (hash * 397) ^ GetPropertiesHashCode(Properties);
            }
            if (Id != null)
            {
                hash = (hash * 397) ^ Id.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(Feature obj)
        {
            return obj.GetHashCode();
        }

        private int GetPropertiesHashCode(Dictionary<string, object> properties)
        {
            var keys = properties.Keys.OrderBy(k => k).ToList();
            int hash = 1;
            string hashString;
            object value;
            foreach (var key in keys)
            {
                value = properties[key];
                hashString = (value == null) ? key : key + value.ToString();
                hash = (hash * 397) ^ hashString.GetHashCode();
            }
            return hash;
        }

        #endregion

    }
}