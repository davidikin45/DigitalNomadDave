using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Solution.Base.Helpers
{
    public class HtmlTagRegister
    {
        private class TagRegister : HtmlTextWriter
        {
            private TagRegister() : base(null) { }

            public static void Register(string tagName)
            {
                RegisterTag(tagName, HtmlTextWriterTag.Unknown);
            }
        }

        public static void RegisterUnknownTag(string tagName)
        {
            TagRegister.Register(tagName);
        }
    }
}
