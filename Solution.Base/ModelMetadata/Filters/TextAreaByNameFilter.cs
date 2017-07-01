using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution.Base.ModelMetadata
{
	public class TextAreaByNameFilter : IModelMetadataFilter
	{
		private static readonly HashSet<string> TextAreaFieldNames =
				new HashSet<string>
						{
							"body",
							"comments",
                            "text"
                        };

		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
			if (!string.IsNullOrEmpty(metadata.PropertyName) &&
				string.IsNullOrEmpty(metadata.DataTypeName) &&
				TextAreaFieldNames.Any(metadata.PropertyName.ToLower().Contains))
			{
				metadata.DataTypeName = "MultilineText";
                metadata.AdditionalValues["MultilineTextRows"] = 7;
                metadata.AdditionalValues["MultilineTextHTML"] = false;
            }
		}
	}
}