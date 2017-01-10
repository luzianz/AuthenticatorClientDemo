using System;
using Newtonsoft.Json;

namespace AuthenticatorClientDemo.JsonConverters
{
    public class UnixTimeConverter : JsonConverter
    {
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override bool CanConvert(Type objectType)
        {
			return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			var unixTime = (long)reader.Value;

			return epoch.AddSeconds(unixTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
			var dt = (DateTime)value;
            var unixTime = (long)dt.Subtract(epoch).TotalSeconds;
			writer.WriteValue(unixTime.ToString());
        }
    }
}