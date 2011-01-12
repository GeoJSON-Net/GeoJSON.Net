// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureCollection.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the FeatureCollection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the FeatureCollection type.
    /// </summary>
    public class FeatureCollection : ICollection<Feature>
    {
        /// <summary>
        /// Locker object for multithreading
        /// </summary>
        private static readonly object featuresLocker = new object();
        
        /// <summary>
        /// Internally used collection to hold features
        /// </summary>
        private readonly IList<Feature> features = new List<Feature>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureCollection"/> class.
        /// </summary>
        /// <param name="features">The features.</param>
        internal FeatureCollection(IList<Feature> features)
        {
            this.features = features;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count of the underlying collection.</value>
        public int Count
        {
            get
            {
                lock (featuresLocker)
                {
                    return this.features.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get
            {
                lock (featuresLocker)
                {
                    return this.features.IsReadOnly;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The Enumerator</returns>
        public IEnumerator<Feature> GetEnumerator()
        {
            lock (featuresLocker)
            {
                return this.features.GetEnumerator();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(Feature item)
        {
            // ToDo: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the underlying collection.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear()
        {
            // ToDo: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the underlying collection contains  the specified item.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>
        /// <c>true</c> if this collection contains the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Feature item)
        {
            lock (featuresLocker)
            {
                return this.features.Contains(item);
            }
        }

        /// <summary>
        /// Copies the elements of the underlying collection to an System.Array, starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(Feature[] array, int arrayIndex)
        {
            lock (featuresLocker)
            {
                this.features.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <returns>
        /// <c>true</c> if item was successfully removed from the collection;
        /// otherwise, <c>false</c>. This method also returns <c>false</c> if item is not found in the original collection.
        /// </returns>
        public bool Remove(Feature item)
        {
            // ToDo: implement
            throw new NotImplementedException();
        }
    }
}
