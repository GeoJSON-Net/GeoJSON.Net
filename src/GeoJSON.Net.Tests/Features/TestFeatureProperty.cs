using System;

namespace GeoJSON.Net.Tests.Features
{
    internal class TestFeatureProperty
    {
        public bool BooleanProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }

        public double DoubleProperty { get; set; }

        public TestFeatureEnum EnumProperty { get; set; }

        public int IntProperty { get; set; }

        public string StringProperty { get; set; }
    }
}
