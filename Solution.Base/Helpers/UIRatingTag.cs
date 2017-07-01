using HtmlTags;

namespace Solution.Base.Helpers
{
	public class UIRatingTag : HtmlTag
	{
        static UIRatingTag()
        {
            HtmlTagRegister.RegisterUnknownTag("uib-rating");
        }

        public UIRatingTag(string model) : base("uib-rating")
		{
			Attr("ng-model", model);
		}

		public UIRatingTag Max(int max)
		{
			Attr("max", max);

			return this;
		}

		public UIRatingTag NgClick(string action)
		{
			Attr("ng-click", action);

			return this;
		}
	}
}