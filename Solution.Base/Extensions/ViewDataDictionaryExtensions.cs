using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Extensions
{
    public static class ViewDataDictionaryExtensions
    {
        public static TAttribute GetModelAttribute<TAttribute>(this ViewDataDictionary viewData, bool inherit = false) where TAttribute : Attribute
        {

            if (viewData == null) throw new ArgumentNullException("viewData");

            var containerType = viewData.ModelMetadata.ContainerType;

            return ((TAttribute[])containerType.GetProperty(viewData.ModelMetadata.PropertyName)
                                                .GetCustomAttributes(typeof(TAttribute), inherit)).FirstOrDefault();

        }

        public static TAttribute GetModelAttribute<TAttribute>(this ViewDataDictionary viewData, string propertyName, bool inherit = false) where TAttribute : Attribute
        {

            if (viewData == null) throw new ArgumentNullException("viewData");

            var containerType = viewData.ModelMetadata.ContainerType;

            return ((TAttribute[])containerType.GetProperty(propertyName)
                                                .GetCustomAttributes(typeof(TAttribute), inherit)).FirstOrDefault();

        }
    }
}
