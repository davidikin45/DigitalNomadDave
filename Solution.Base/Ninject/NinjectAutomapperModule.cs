using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ninject.Modules;
using Solution.Base.Automapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Conventions;
using System.Security.Principal;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Web.Common;

namespace Solution.Base.Ninject
{
    public class NinjectAutomapperModule : NinjectModule
    {
        Func<Assembly, Boolean> _filter;
        public NinjectAutomapperModule(Func<Assembly, Boolean> filter)
        {
            _filter = filter;
        }
        
        public override void Load()
        {      
            var config = new MapperConfiguration(cfg => {
                new AutoMapperConfiguration(cfg, _filter);
            });

            Bind<MapperConfiguration>().ToConstant(config);
            Bind<IConfigurationProvider>().ToMethod(ctx => config);
            Bind<IExpressionBuilder>().ToConstructor(ctx => new ExpressionBuilder(config));

            Bind<IMapper>().ToMethod(c => config.CreateMapper()).InSingletonScope();

        }
    }
}
