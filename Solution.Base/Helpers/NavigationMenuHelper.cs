using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Solution.Base.Helpers
{
    public static class NavigationMenuHelperExtension
    {
        public static NavigationMenuHelper NavigationMenu(this HtmlHelper helper)
        {
            return new NavigationMenuHelper();
        }

        public static NavigationMenuHelper NavigationMenu(this UrlHelper helper)
        {
            return new NavigationMenuHelper();
        }

        public class NavigationMenuHelper
        {
            private static Lazy<object> _menu = new Lazy<dynamic>(() => JsonConvert.DeserializeObject(File.ReadAllText(HostingEnvironment.MapPath("~/Views/Shared/Navigation/navigation.json"))));

            public dynamic Menu
            {
                get { return _menu.Value; }
            }

            private static Lazy<object> _adminMenu = new Lazy<dynamic>(() => JsonConvert.DeserializeObject(File.ReadAllText(HostingEnvironment.MapPath("~/Views/Shared/Navigation/admin-navigation.json"))));

            public dynamic AdminMenu
            {
                get { return _adminMenu.Value; }
            }
        }
    }
}
