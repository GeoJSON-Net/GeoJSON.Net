namespace GeoJSON.Net.Tests.Feature
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The Test Property Dictionary object.
    /// </summary>
    internal class TestFeaturePropertyDictionary : IDictionary<string, object>
    {
        /// <summary>
        /// The internal dictionary this implementation is wrapping for testing purposes.
        /// </summary>
        private readonly IDictionary<string, object> internalDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFeaturePropertyDictionary"/> class.
        /// </summary>
        public TestFeaturePropertyDictionary()
        {
            this.internalDictionary = new Dictionary<string, object>();
        }

        public bool BooleanProperty
        {
            get
            {
                return this.GetKeyOrDefault<bool>(nameof(this.BooleanProperty));
            }

            set
            {
                this.internalDictionary[nameof(this.BooleanProperty)] = value;
            }
        }

        public DateTime DateTimeProperty
        {
            get
            {
                return this.GetKeyOrDefault<DateTime>(nameof(this.DateTimeProperty));
            }

            set
            {
                this.internalDictionary[nameof(this.DateTimeProperty)] = value;
            }
        }

        public double DoubleProperty
        {
            get
            {
                return this.GetKeyOrDefault<double>(nameof(this.DoubleProperty));
            }

            set
            {
                this.internalDictionary[nameof(this.DoubleProperty)] = value;
            }
        }

        public TestFeatureEnum EnumProperty
        {
            get
            {
                return this.GetKeyOrDefault<TestFeatureEnum>(nameof(this.EnumProperty));
            }

            set
            {
                this.internalDictionary[nameof(this.EnumProperty)] = value;
            }
        }

        public int IntProperty
        {
            get
            {
                return this.GetKeyOrDefault<int>(nameof(this.IntProperty));
            }

            set
            {
                this.internalDictionary[nameof(this.IntProperty)] = value;
            }
        }

        public string StringProperty
        {
            get
            {
                return this.GetKeyOrDefault<string>(nameof(this.StringProperty));
            }

            set
            {
                this.internalDictionary[nameof(this.StringProperty)] = value;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                return this.internalDictionary.Count;
            }
        }

        /// <inheritdoc/>
        public bool IsReadOnly
        {
            get
            {
                return this.internalDictionary.IsReadOnly;
            }
        }

        /// <inheritdoc/>
        public ICollection<string> Keys
        {
            get
            {
                return this.internalDictionary.Keys;
            }
        }

        /// <inheritdoc/>
        public ICollection<object> Values
        {
            get
            {
                return this.internalDictionary.Values;
            }
        }

        /// <inheritdoc/>
        public object this[string key]
        {
            get
            {
                return this.internalDictionary[key];
            }

            set
            {
                this.internalDictionary[key] = value;
            }
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.internalDictionary.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<string, object> item)
        {
            this.internalDictionary.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.internalDictionary.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.internalDictionary.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            this.internalDictionary.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.internalDictionary.Remove(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            return this.internalDictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void Add(string key, object value)
        {
            this.internalDictionary.Add(key, value);
        }

        /// <inheritdoc/>
        public bool Remove(string key)
        {
            return this.internalDictionary.Remove(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out object value)
        {
            return this.internalDictionary.TryGetValue(key, out value);
        }

        private T GetKeyOrDefault<T>(string keyName)
        {
            object value;
            if (this.TryGetValue(keyName, out value))
            {
                return (T)value;
            }

            return default(T);
        }
    }
}
