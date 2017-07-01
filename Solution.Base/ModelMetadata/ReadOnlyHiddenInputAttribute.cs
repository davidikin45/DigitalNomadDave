using System;
using System.Web.Mvc;

namespace Solution.Base.ModelMetadata
{
    //ReadOnly with Hidden Input or Normal
    public class ReadOnlyHiddenInputAttribute : Attribute, IMetadataAware
    {
        public bool ShowForEdit { get; set; }
        public bool ShowForCreate { get; set; }

        public ReadOnlyHiddenInputAttribute()
        {
            ShowForEdit = true;
            ShowForCreate = true;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.AdditionalValues["ReadOnlyHiddenInputEdit"] = ShowForEdit;
            metadata.AdditionalValues["ReadOnlyHiddenInputCreate"] = ShowForCreate;
        }
    }
}