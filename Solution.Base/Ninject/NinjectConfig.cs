﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Configuration;

namespace Solution.Base.Ninject
{
    //BindDefaultInterface() - public class Foo : IFoo
    //BindDefaultInterfaces() - public class SuperFoo : IFoo, ISuperFoo
    //BindAllInterfaces() - public class Foo : IWhatever
    public static class NinjectConfig
    {
        public static IKernel BuildKernel<TUser>(string name)
            where TUser : IdentityUser, IUser
        {
            string binPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            string pluginsPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), "plugins\\");
            if (!Directory.Exists(pluginsPath)) Directory.CreateDirectory(pluginsPath);

            var assemblyPrefix = ConfigurationManager.AppSettings["AssemblyPrefix"];

            Func<Assembly, Boolean> filterFunc = (a => a.FullName.Contains(name) || a.FullName.Contains(assemblyPrefix) || a.FullName.Contains("Solution"));
            Func<Type, Boolean> filter = (a => a.FullName.Contains(name) || a.FullName.Contains(assemblyPrefix) || a.FullName.Contains("Solution"));

            var modules = new INinjectModule[]
           {
                new NinjectMVCModule<TUser>(),
                new NinjectConventionsModule(new string[] {binPath, pluginsPath}, filter),
                new NinjectTaskModule(new string[] {binPath, pluginsPath}, filter),
                new NinjectAutomapperModule(filterFunc),
                new NinjectModelMetadataModule(new string[] {binPath, pluginsPath}, filter)
           };
            var kernel= new StandardKernel(modules);

            //NinjectWeb.RegisterServices(kernel, name);

            //string pluginsPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), "plugins\\");
            //if (!Directory.Exists(pluginsPath)) Directory.CreateDirectory(pluginsPath);

            //Predicate<Assembly> filterPredictate = (a => a.FullName.Contains(name) || a.FullName.Contains("Solution"));
            //Func<Assembly, Boolean> filterFunc = (a => a.FullName.Contains(name) || a.FullName.Contains("Solution"));

            //kernel.Bind(x => x
            //  .FromAssembliesInPath(pluginsPath, filterPredictate)
            //  .SelectAllClasses()
            //  .BindDefaultInterface());

            return kernel;
        }
    }
}
