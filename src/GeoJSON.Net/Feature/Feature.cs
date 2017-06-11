// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System.Collections.Generic;
#if (!NET35 || !NET40)
using System.Reflection;
using System.Linq;
#endif
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;

namespace GeoJSON.Net.Feature
{
    /// <summary>
    /// A GeoJSON Feature Object; generic version for strongly typed <see cref="Geometry"/>
    /// and <see cref="Properties"/>
    /// </summary>
    /// <remarks>
    /// See https://tools.ietf.org/html/rfc7946#section-3.2
    /// </remarks>
    public class Feature<TGeometry, TProps> : GeoJSONObject, IEquatable<Feature<TGeometry, TProps>>
        where TGeometry : IGeometryObject
    {
        [JsonConstructor]
        public Feature(TGeometry geometry, TProps properties, string id = null)
        {
            Geometry = geometry;
            Properties = properties;
            Id = id;
        }
        
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; }
        
        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public TGeometry Geometry { get; }
        
        [JsonProperty(PropertyName = "properties", Required = Required.AllowNull)]
        public TProps Properties { get;}
        
        /// <summary>
        /// Equality comparer.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="Feature.Equals(Feature)"/>, this implementation returns true only
        /// if <see cref="Id"/> and <see cref="Properties"/> are also equal. See
        /// <a href="https://github.com/GeoJSON-Net/GeoJSON.Net/issues/80">#80</a> for discussion. The rationale
        /// here is that a user explicitly specifying the property type most probably cares about the properties
        /// equality.
        /// </remarks>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Feature<TGeometry, TProps> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && string.Equals(Id, other.Id)
                   && EqualityComparer<TGeometry>.Default.Equals(Geometry, other.Geometry)
                   && EqualityComparer<TProps>.Default.Equals(Properties, other.Properties);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Feature<TGeometry, TProps>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EqualityComparer<TGeometry>.Default.GetHashCode(Geometry);
                hashCode = (hashCode * 397) ^ EqualityComparer<TProps>.Default.GetHashCode(Properties);
                return hashCode;
            }
        }

        public static bool operator ==(Feature<TGeometry, TProps> left, Feature<TGeometry, TProps> right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(Feature<TGeometry, TProps> left, Feature<TGeometry, TProps> right)
        {
            return !object.Equals(left, right);
        }
    }
    
    
    /// <summary>
    /// A GeoJSON Feature Object.
    /// </summary>
    /// <remarks>
    /// See https://tools.ietf.org/html/rfc7946#section-3.2
    /// </remarks>
    public class Feature : GeoJSONObject, IEqualityComparer<Feature>, IEquatable<Feature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feature" /> class.
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
        /// Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">
        /// Class used to fill feature properties. Any public member will be added to feature
        /// properties
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
#if(NET35 || NET40)
                Properties = properties.GetType().GetProperties()
                    .Where(propertyInfo => propertyInfo.GetGetMethod().IsPublic)
                    .ToDictionary(propertyInfo => propertyInfo.Name,
                        propertyInfo => propertyInfo.GetValue(properties, null));
#else
                Properties = properties.GetType().GetTypeInfo().DeclaredProperties
                    .Where(propertyInfo => propertyInfo.GetMethod.IsPublic)
                    .ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(properties, null));
#endif
            }

            Type = GeoJSONObjectType.Feature;
        }

        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        /// <value>
        /// The geometry.
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
        /// Gets the properties.
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
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(Feature obj)
        {
            return obj.GetHashCode();
        }

#endregion

    }
}