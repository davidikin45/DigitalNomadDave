using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Alerts
{
    [DataContract]
    public class WebApiMessage
    {
        public static WebApiMessage CreateWebApiMessage(string message, IList<string> errors, Object id)
        {
            return new WebApiMessage(message, errors.ToArray(), id);
        }

        private WebApiMessage(string message, string[] errors, Object id)
        {
            this.Message = message;
            this.Errors = errors;
            this.Id = id;
        }

        public static WebApiMessage CreateWebApiMessage(string message, IList<string> errors)
        {
            return new WebApiMessage(message, errors.ToArray());
        }

        private WebApiMessage(string message, string[] errors)
        {
            this.Message = message;
            this.Errors = errors;
        }

        public static WebApiMessage CreateWebApiMessage(string message, IList<string> errors, System.Web.Mvc.ModelStateDictionary modelState)
        {
            return new WebApiMessage(message, errors.ToArray(), modelState);
        }

        private WebApiMessage(string message, string[] errors, System.Web.Mvc.ModelStateDictionary modelState)
        {
            this.Message = message;
            this.Errors = errors;

            this.ModelState = new Dictionary<string, List<string>>();

            if (modelState != null)
            {

                foreach (KeyValuePair<string, System.Web.Mvc.ModelState> property in modelState)
                {
                    var propertyMessages = new List<string>();
                    foreach (System.Web.Mvc.ModelError modelError in property.Value.Errors)
                    {
                        propertyMessages.Add(modelError.ErrorMessage);
                    }
                    this.ModelState.Add(property.Key, propertyMessages);
                }

            }
        }

        public static WebApiMessage CreateWebApiMessage(string message, IList<string> errors, System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            return new WebApiMessage(message, errors.ToArray(), modelState);
        }

        private WebApiMessage(string message, string[] errors, System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            this.Message = message;
            this.Errors = errors;

            this.ModelState = new Dictionary<string, List<string>>();
            if (modelState != null)
            {
                foreach (KeyValuePair<string, System.Web.Http.ModelBinding.ModelState> property in modelState)
                {
                    var propertyMessages = new List<string>();
                    foreach (System.Web.Http.ModelBinding.ModelError modelError in property.Value.Errors)
                    {
                        propertyMessages.Add(modelError.ErrorMessage);
                    }
                    this.ModelState.Add(property.Key, propertyMessages);
                }
            }

            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
            //        "ModelState": {
            //            "item": [
            //              "Required property 'Name' not found in JSON. Path '', line 1, position 14."
            //],
            //"item.Name": [
            //  "The Name field is required."
            //],
            //"item.Price": [
            //  "The field Price must be between 0 and 999."
            //]
        }

        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Object Id { get; set; }

        [DataMember]
        public Boolean Success {
            get {
                return Errors.Count() == 0;
            }
            private set { }
        }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public string Error
        {
            get
            {
                return string.Join("\n", Errors);
            }
            private set { }
        }

        [DataMember]
        public string[] Errors { get; private set; }

        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, List<string>> ModelState { get; private set; }

    }
}
