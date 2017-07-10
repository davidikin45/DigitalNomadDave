using Solution.Base.Implementation.Models;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Solution.Base.Extensions;
using Solution.Base.Interfaces.Model;
using System.Linq;

namespace Solution.Base.ModelMetadata
{
    public class MultilineTextAttribute : Attribute, IMetadataAware
    {
        public int Rows { get; set; }
        public Boolean HTML { get; set; }

        public MultilineTextAttribute()
        {
            Rows = 7;
            HTML = false;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.DataTypeName = "MultilineText";
            metadata.AdditionalValues["MultilineTextRows"] = Rows;
            metadata.AdditionalValues["MultilineTextHTML"] = HTML;
        }
    }
}