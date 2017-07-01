using Solution.Base.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace Solution.Base.Infrastructure
{
    public class WebApiAssemblyResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            //https://books.google.co.uk/books?id=7aE8BAAAQBAJ&pg=PA132&lpg=PA132&dq=web+api+load+controller+from+different+assembly&source=bl&ots=fv-2SV0pNI&sig=lwceABSKb5zSxY57kE6D1lQb85M&hl=en&sa=X&ved=0ahUKEwic2vuJp57TAhXrE5oKHeNhAzg4FBDoAQgtMAM#v=onepage&q=web%20api%20load%20controller%20from%20different%20assembly&f=false
            var baseAssemblies = base.GetAssemblies().ToList();
            var assemblies = new List<Assembly>(baseAssemblies) { typeof(ContentTextController).Assembly };

            return baseAssemblies.Distinct().ToList();
        }
    }
}
