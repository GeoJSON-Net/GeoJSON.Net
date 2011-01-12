// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParsingException.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the ParsingException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Exceptions
{
    using System;

    /// <summary>
    /// Exception raised when response from SimpleGeo API cannot be parsed
    /// </summary>
    public class ParsingException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ParsingException(string message = "Could not parse GeoJSON Response.", Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
