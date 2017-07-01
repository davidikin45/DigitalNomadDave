using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;

namespace Solution.Base.ModelMetadata
{
	public class UserIDDropDownByNameFilter : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
			if (!string.IsNullOrEmpty(metadata.PropertyName) &&
				string.IsNullOrEmpty(metadata.DataTypeName) &&
				metadata.PropertyName.ToLower().Contains("assignedto"))
			{
				metadata.DataTypeName = "UserID";
			}
		}
	}
}