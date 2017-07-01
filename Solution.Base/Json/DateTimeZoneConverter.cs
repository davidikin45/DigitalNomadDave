using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Solution.Base.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Json
{
    public class DateTimeZoneConverter : DateTimeConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            var dateTime = DateTime.Parse(reader.Value.ToString());
            return dateTime.FromConfigLocalTimeToUTC();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var localDT = ((DateTime)value).ToConfigLocalTime();
            writer.WriteValue(localDT);
        }
    }
}
