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
            internalDictionary = new Dictionary<string, object>();
        }

        public bool BooleanProperty
        {
            get
            {
                return GetKeyOrDefault<bool>(nameof(BooleanProperty));
            }

            set
            {
                internalDictionary[nameof(BooleanProperty)] = value;
            }
        }

        public DateTime DateTimeProperty
        {
            get
            {
                return GetKeyOrDefault<DateTime>(nameof(DateTimeProperty));
            }

            set
            {
                internalDictionary[nameof(DateTimeProperty)] = value;
            }
        }

        public double DoubleProperty
        {
            get
            {
                return GetKeyOrDefault<double>(nameof(DoubleProperty));
            }

            set
            {
                internalDictionary[nameof(DoubleProperty)] = value;
            }
        }

        public TestFeatureEnum EnumProperty
        {
            get
            {
                return GetKeyOrDefault<TestFeatureEnum>(nameof(EnumProperty));
            }

            set
            {
                internalDictionary[nameof(EnumProperty)] = value;
            }
        }

        public int IntProperty
        {
            get
            {
                return GetKeyOrDefault<int>(nameof(IntProperty));
            }

            set
            {
                internalDictionary[nameof(IntProperty)] = value;
            }
        }

        public string StringProperty
        {
            get
            {
                return GetKeyOrDefault<string>(nameof(StringProperty));
            }

            set
            {
                internalDictionary[nameof(StringProperty)] = value;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                return internalDictionary.Count;
            }
        }

        /// <inheritdoc/>
        public bool IsReadOnly
        {
            get
            {
                return internalDictionary.IsReadOnly;
            }
        }

        /// <inheritdoc/>
        public ICollection<string> Keys
        {
            get
            {
                return internalDictionary.Keys;
            }
        }

        /// <inheritdoc/>
        public ICollection<object> Values
        {
            get
            {
                return internalDictionary.Values;
            }
        }

        /// <inheritdoc/>
        public object this[string key]
        {
            get
            {
                return internalDictionary[key];
            }

            set
            {
                internalDictionary[key] = value;
            }
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return internalDictionary.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<string, object> item)
        {
            internalDictionary.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            internalDictionary.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return internalDictionary.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            internalDictionary.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<string, object> item)
        {
            return internalDictionary.Remove(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            return internalDictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void Add(string key, object value)
        {
            internalDictionary.Add(key, value);
        }

        /// <inheritdoc/>
        public bool Remove(string key)
        {
            return internalDictionary.Remove(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out object value)
        {
            return internalDictionary.TryGetValue(key, out value);
        }

        private T GetKeyOrDefault<T>(string keyName)
        {
            object value;
            if (TryGetValue(keyName, out value))
            {
                return (T)value;
            }

            return default(T);
        }
    }
}
