using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BibliotecaLibro.Shared.Json
{
    public class ConversorJsonFechaOnly : JsonConverter<DateOnly>
    {
        private const string Formato = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            if (string.IsNullOrWhiteSpace(s)) return default;

            if (DateOnly.TryParse(s, out var d)) return d;
            if (DateTime.TryParse(s, out var dt)) return DateOnly.FromDateTime(dt);

            throw new JsonException($"Fecha inválida: {s}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(Formato));
    }
}
