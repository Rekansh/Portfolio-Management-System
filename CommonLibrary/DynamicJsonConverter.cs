using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonLibrary
{
    /// <summary>
    /// A custom JSON converter for dynamic properties.
    /// </summary>
    public class DynamicJsonConverter : JsonConverter<dynamic>
    {
        public override dynamic Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            return ConvertJsonElementToDictionary(jsonElement);
        }

        public override void Write(Utf8JsonWriter writer, dynamic value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private static object ConvertJsonElementToDictionary(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                var dictionary = new Dictionary<string, object>();
                foreach (var property in element.EnumerateObject())
                {
                    dictionary[property.Name] = ConvertJsonElementToDictionary(property.Value);
                }
                return dictionary;
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                var list = new List<object>();
                foreach (var item in element.EnumerateArray())
                {
                    list.Add(ConvertJsonElementToDictionary(item));
                }
                return list;
            }
            else
            {
                return element.ValueKind switch
                {
                    JsonValueKind.String => element.GetString(),
                    JsonValueKind.Number => element.GetDouble(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => null,
                    _ => element.GetRawText(),
                };
            }
        }
    }
}
