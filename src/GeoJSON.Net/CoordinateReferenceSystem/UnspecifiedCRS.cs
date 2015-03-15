namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    /// Represents an unspecified Coordinate Reference System 
    /// i.e. where a geojson object has a null crs
    /// </summary>
    public class UnspecifiedCRS : ICRSObject
    {
        /// <summary>
        /// Gets the CRS type.
        /// </summary>
        public CRSType Type
        {
            get
            {
                return CRSType.Unspecified;
            }
        }
    }
}