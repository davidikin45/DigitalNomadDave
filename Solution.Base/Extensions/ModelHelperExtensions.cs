using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Solution.Base.Extensions
{
    public static class ModelHelperExtensions
    {
        //Model Type
        public static Type ModelType(this ViewDataDictionary viewData)
        {
            return ModelType(viewData.Model);
        }

        public static Type ModelType(this object model)
        {
            var type = model.GetType();
            var ienum = type.GetInterface(typeof(IEnumerable<>).Name);
            type = ienum != null
              ? ienum.GetGenericArguments()[0]
              : type;
            return type;
        }

        //ModelMetadata
        public static System.Web.Mvc.ModelMetadata ModelMetadata(this ViewDataDictionary viewData)
        {
            return ModelMetadata(viewData.Model);
        }

        public static System.Web.Mvc.ModelMetadata ModelMetadata(this ViewDataDictionary viewData, object model)
        {
            return ModelMetadata(model);
        }

        public static System.Web.Mvc.ModelMetadata ModelMetadata(this object model)
        {
            var type = ModelType(model);
            var modelMetaData = ModelMetadataProviders.Current.GetMetadataForType(null, type);
            return modelMetaData;
        }

        //Labels
        public static IHtmlString DisplayName(this HtmlHelper html, object model, string propertyName)
        {
            return DisplayName(model, propertyName);
        }

        public static String EnumDisplayName(this object e)
        {
            FieldInfo fieldInfo = e.GetType().GetField(e.ToString());
            DisplayAttribute[] displayAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            return null != displayAttributes && displayAttributes.Length > 0 ? displayAttributes[0].Name : e.ToString();
        }

        public static IHtmlString DisplayName(this object model, string propertyName)
        {
            Type type = ModelType(model);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForProperty(null, type, propertyName);
            var value = modelMetadata.DisplayName ?? modelMetadata.PropertyName;
            return new HtmlString(HttpUtility.HtmlEncode(value));
        }

        //Display
        public static IHtmlString Display<T>(this HtmlHelper html, T model, string propertyName)
        {
            HtmlHelper<T> newHtml = html.For<T>(model);
            return newHtml.Display(propertyName);
        }

        public static IHtmlString Display(this HtmlHelper html, dynamic model, string propertyName)
        {
            HtmlHelper<dynamic> newHtml = HtmlHelperExtensions.For(html, model);
            return newHtml.Display(propertyName);
        }

        //Values
        public static IHtmlString DisplayTextSimple(this HtmlHelper html, string propertyName)
        {
            return DisplayTextSimple(html.ViewData.Model, propertyName);
        }

        public static IHtmlString DisplayTextSimple(this HtmlHelper html, object model, string propertyName)
        {
            return DisplayTextSimple(model, propertyName);
        }

        public static IHtmlString DisplayTextSimple(this object model, string propertyName)
        {
            Type type = ModelType(model);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, type);

            var propertyMetadata = (from p in modelMetadata.Properties
                                    where p.PropertyName == propertyName
                                    select p).FirstOrDefault<System.Web.Mvc.ModelMetadata>();

            string value = "";

            if (propertyMetadata != null)
            {
                value = propertyMetadata.SimpleDisplayText;
                if (propertyMetadata.HtmlEncode)
                {
                    value = HttpUtility.HtmlEncode(value);
                }
            }

            return new HtmlString(value);
        }

        public static IHtmlString DisplayFormatString(this object model, string propertyName)
        {
            Type type = ModelType(model);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, type);

            var propertyMetadata = (from p in modelMetadata.Properties
                                    where p.PropertyName == propertyName
                                    select p).FirstOrDefault<System.Web.Mvc.ModelMetadata>();

            string value = "";

            if (propertyMetadata != null)
            {
                value = propertyMetadata.DisplayFormatString;
                if (propertyMetadata.HtmlEncode)
                {
                    value = HttpUtility.HtmlEncode(value);
                }
            }

            return new HtmlString(value);
        }

    }
}
