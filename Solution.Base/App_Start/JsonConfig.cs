using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Globalization;
using Solution.Base.Json;

namespace Solution.Base.App_Start
{
    public class JsonConfig
    {
        public static void Init()
        {
            //http://www.seankenny.me/blog/2013/09/19/web-api-entity-framework-and-is08601-date-formats/
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/json-and-xml-serialization

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
           
            settings.Culture = System.Globalization.CultureInfo.CurrentCulture;
            //settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Converters = new JsonConverter[] {
                new StringEnumConverter()
               //,new DateTimeZoneConverter()
            };
            settings.Formatting = Formatting.Indented;
            settings.NullValueHandling = NullValueHandling.Include;

            JsonConvert.DefaultSettings = () => settings;

            //jsonFormatter.SerializerSettings = settings;
            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //    Converters = new JsonConverter[] { new StringEnumConverter() },
            //    Formatting = Formatting.Indented,
            //    NullValueHandling = NullValueHandling.Include
            //};
        }


    }
}
