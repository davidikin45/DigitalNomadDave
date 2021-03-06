﻿
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using System.IO;
using Solution.Base.Tasks;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Solution.Base.Ninject
{
    [Serializable]
    public class NinjectTaskModule : NinjectModule
    {
        string[] _paths;
        Func<Type, Boolean> _filter;
        public NinjectTaskModule(string[] paths, Func<Type, Boolean> filter)
        {
            _paths = paths;
            _filter = filter;
        }

        public override void Load()
        {

            var types = new List<Type>();
            types.Add(typeof(IRunAtInit));
            types.Add(typeof(IRunAtStartup));
            types.Add(typeof(IRunAtOwinStartup));
            types.Add(typeof(IRunOnEachRequest));
            types.Add(typeof(IRunOnError));
            types.Add(typeof(IRunAfterEachRequest));

            foreach (string _path in _paths)
            {
                this.Bind(x => x
                             .FromAssembliesInPath(_path)
                             .SelectAllClasses().InheritedFromAny(types).Where(_filter)
                            .BindAllInterfaces());

            }
        }
    }
}