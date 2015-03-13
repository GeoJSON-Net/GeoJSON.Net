using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoJSON.Net.Geometry
{
    /// <summary>
    /// This is a wrapper around a geometry object that allows for assigning 
    /// a key.
    /// </summary>
    public class TopoJSONNamedObjectWrapper : INamedGeometryObject
    {
        /// <summary>
        /// This is the full constructor.
        /// </summary>
        /// <param name="g">The geometry.</param>
        /// <param name="name">The object name.</param>
        public TopoJSONNamedObjectWrapper(string name, IGeometryObject g) {
            this.Name = name;
            this.Geometry = g;
        }

        #region ---------- Name ----------
        /// <summary>
        /// A unique Id. This is needed for named (sub-) objects
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region ---------- Geometry ----------
        /// <summary>
        /// The Geometry.
        /// </summary>
        public IGeometryObject Geometry { get; set; }
        #endregion
    }
}
