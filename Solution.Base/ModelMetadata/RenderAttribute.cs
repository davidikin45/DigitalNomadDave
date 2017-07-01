using System;
using System.Web.Mvc;

namespace Solution.Base.ModelMetadata
{
    public class RenderAttribute : Attribute, IMetadataAware
    {
        public bool ShowForGrid { get; set; }
        public bool AllowSortForGrid { get; set; }
        public bool ShowForEdit { get; set; }
        private bool _showForCreateSet;
        private bool _showForCreate;
        public bool ShowForCreate
        {
            get { return _showForCreate; }
            set
            {
                _showForCreateSet = true;
                _showForCreate = value;
            }
        }
        public bool ShowForDisplay { get; set; }

        public RenderAttribute()
        {
            ShowForGrid = true;
            AllowSortForGrid = true;
            ShowForEdit = true;
            _showForCreate = true;
            ShowForDisplay = true;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.AdditionalValues["ShowForGrid"] = ShowForGrid;
            metadata.AdditionalValues["AllowSortForGrid"] = AllowSortForGrid;
            metadata.ShowForEdit = ShowForEdit;

            if (!_showForCreateSet)
            {
                _showForCreate = ShowForEdit;
            }

            metadata.AdditionalValues["ShowForCreate"] = _showForCreate;
            metadata.ShowForDisplay = ShowForDisplay;
        }
    }
}