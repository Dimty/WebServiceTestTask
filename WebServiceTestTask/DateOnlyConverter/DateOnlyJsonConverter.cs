using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebServiceTestTask
{
    /// <summary>
    /// Allows you to serialize the
    /// <see cref="DateOnly">DateOnly</see> format.
    /// </summary>
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        /// <summary>
        /// Reads and converts JSON to type DateOnly.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="typeToConvert">The type that is being converted.</param>
        /// <param name="options">An object that specifies the serialization parameters to be used.</param>
        /// <returns>Converted DateOnly value.</returns>
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Writes the specified value in JSON format.
        /// </summary>
        /// <param name="writer">The recording module to which the recording is being performed.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies the serialization parameters to be used.</param>
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
