using DND.Services.SearchEngines;
using DND.Services.SearchEngines.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services.Factories
{
    public class FlightSearchEngineFactory
    {
        public IFlightSearchEngine GetFlightSearchEngine(string id)
        {
            switch (id)
            {
                case "skyscanner":
                   

                    string dataPath = "";
                    if (System.Web.HttpContext.Current != null)
                    {
                        dataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Standing_Data/Skyscanner/");
                    }
                    else
                    {
                        throw new Exception("Not implemented");
                    }

                    return new SkyscannerSearchEngine(dataPath, "prtl6749387986743898559646983194", "fl616387654491797334414383167645", "fl571074181652076367336532635378", "fl568057102257161227951936116720", "fl509894289486958690857440606394",
                                                     "fl793017796388079205083378399982", "fl125488904091534567725289422462", "fl831406883824771121447625248426", "fl415899869067333737232433164515",
                                                     "fl495359687731285699830198352149", "fl114374776285956211333821687650", "fl423566323123756550575229167754");

                    //return new SkyscannerSearchEngine(dataPath, "prtl6749387986743898559646983194");
                    //https://support.business.skyscanner.net/hc/en-us/articles/206800359-Which-services-can-I-access-and-what-are-the-rate-limits-
                    //https://support.business.skyscanner.net/hc/en-us/articles/207322339-Do-you-offer-commissions-for-use-of-your-White-Label-or-API-
                    //prtl6749387986743898559646983194
            }

            throw new Exception("Unknown flight search engine");
        }
    }
}
