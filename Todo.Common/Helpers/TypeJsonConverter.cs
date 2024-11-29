using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Todo.Common.Helpers
{
    public class TypeJsonConverter : JsonConverter<Type>
    {
        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize a Type from a string (e.g., type name)
            return Type.GetType(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            // Serialize a Type to a string (e.g., type name)
            writer.WriteStringValue(value?.AssemblyQualifiedName);
        }
    }

}
