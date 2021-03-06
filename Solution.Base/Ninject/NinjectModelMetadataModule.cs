﻿using System;
using System.IO;
using System.Web.Mvc;
using Ninject.Extensions.Conventions;
using System.Reflection;
using Ninject.Modules;
using Solution.Base.ModelMetadata;

namespace Solution.Base.Ninject
{
    public class NinjectModelMetadataModule : NinjectModule
    {

        string[] _paths;
        Func<Type, Boolean> _filter;
        public NinjectModelMetadataModule(string[] paths, Func<Type, Boolean> filter)
        {
            _paths = paths;
            _filter = filter;
        }

        public override void Load()
        {
            Bind<ModelMetadataProvider>().To<ExtensibleModelMetadataProvider>();

            foreach (string _path in _paths)
            {
                this.Bind(x => x
                              .FromAssembliesInPath(_path)
                              .SelectAllClasses().InheritedFrom<IModelMetadataFilter>().Where(_filter)
                              .BindAllInterfaces());
            }
        }
    }
}