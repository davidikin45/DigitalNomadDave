using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution.Base.ModelMetadata
{
	public class HtmlByNameFilter : IModelMetadataFilter
	{
		private static readonly HashSet<string> TextAreaFieldNames =
				new HashSet<string>
						{
							"html"
						};

		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
			if (!string.IsNullOrEmpty(metadata.PropertyName) &&
				string.IsNullOrEmpty(metadata.DataTypeName) &&
				TextAreaFieldNames.Any(metadata.PropertyName.ToLower().Contains))
			{
				metadata.DataTypeName = "Html";
			}
		}
	}
}