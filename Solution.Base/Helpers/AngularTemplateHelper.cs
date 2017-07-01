using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Helpers
{
    public static class AngularTemplateHelper
    {
        private static readonly Dictionary<Type, string> TemplateMap
            = new Dictionary<Type, string>
        {
            {typeof (byte), "Number"},
            {typeof (sbyte), "Number"},
            {typeof (int), "Number"},
            {typeof (uint), "Number"},
            {typeof (long), "Number"},
            {typeof (ulong), "Number"},
            {typeof (bool), "Boolean"},
            {typeof (decimal), "Decimal"},
        };

        public static string GetTemplateForProperty(System.Web.Mvc.ModelMetadata propertyMetadata)
        {
            var templateName = propertyMetadata.TemplateHint ??
                propertyMetadata.DataTypeName;

            if (templateName == null)
            {
                templateName = TemplateMap.ContainsKey(propertyMetadata.ModelType) ?
                    TemplateMap[propertyMetadata.ModelType] :
                    propertyMetadata.ModelType.Name;
            }

            return "Angular/" + templateName;
        }
    }
}
