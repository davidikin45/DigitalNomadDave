using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ninject.Modules;
using Solution.Base.Automapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject.Extensions.Conventions;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Web;
using Ninject;

namespace Solution.Base.Ninject
{
    [Serializable()]
    public class NinjectConventionsModule : NinjectModule
    {
        string[] _paths;
        Func<Type, Boolean> _filter;
        public NinjectConventionsModule(string[] paths, Func<Type, Boolean> filter)
        {
            _paths = paths;
            _filter = filter;
        }

        public override void Load()
        {
            //Will only bind to referenced assemblies
            //kernel.Bind(x => x.FromAssembliesMatching("DND.*.dll")
            //   .SelectAllClasses()
            // .BindDefaultInterface()
            //  );
            foreach (string _path in _paths)
            {
                this.Bind(x => x
              .FromAssembliesInPath(_path)
              .SelectAllClasses().Where(_filter)
              .BindDefaultInterface());
            }
        }
    }
}
