using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoJSON.Net.Geometry
{
    /// <summary>
    /// Holds all information about quantization such as translation and scale.
    /// </summary>
    public class TopoJSONTransformationInfo
    {
        #region ---------- IsQuantized ----------
        /// <summary>
        /// Is the object quantized?
        /// </summary>
        public bool isQuantized { get; set; }
        #endregion

        #region ---------- Translation ----------
        /// <summary>
        /// The translation values.
        /// </summary>
        public double[] Translation { get; set; }
        #endregion

        #region ---------- Scale ----------
        /// <summary>
        /// An array holding the scale factors.
        /// </summary>
        public double[] Scale { get; set; }
        #endregion

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public TopoJSONTransformationInfo()
        {

        }
    }
}
