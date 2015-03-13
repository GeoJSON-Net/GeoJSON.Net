using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoJSON.Net.Geometry
{
    /// <summary>
    /// In order to properly build a topology we need a name key for each object.
    /// </summary>
    public class INamedGeometryObject
    {
        #region ---------- Name ----------
        /// <summary>
        /// A unique Id. This is needed for named (sub-) objects
        /// </summary>
        string Name { get; set; }
        #endregion
    }
}
