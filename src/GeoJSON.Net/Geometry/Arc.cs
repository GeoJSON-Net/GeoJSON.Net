using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// An arc is an array of positions from which other parts of the topology can be 
    /// constructed. See also: https://github.com/topojson/topojson-specification/blob/master/README.md#213-arcs
    /// </summary>
    public class Arc
    {
        private List<GeographicPosition> _positions;
        /// <summary>
        /// The positions.
        /// </summary>
        public List<GeographicPosition> Positions
        {
            get {
                return this._positions;
            }
            set {
                if (value != this._positions)
                    this._positions = value;
            }
        }

        /// <summary>
        /// Constructs an arc from a given list of positions. The must be at least two positions.
        /// </summary>
        /// <param name="positions">The positions.</param>
        public Arc(List<GeographicPosition> positions) {
            this.Positions = positions;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Arc() { }
    }
}
