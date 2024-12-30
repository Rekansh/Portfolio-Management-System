using System;
using System.Text.Json.Serialization;

namespace CommonLibrary
{
    /// <summary>
    /// Custom attribute to apply DynamicJsonConverter for dynamic properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonConverter : JsonConverterAttribute
    {
        public JsonConverter() : base(typeof(DynamicJsonConverter))
        {
        }
    }
}
