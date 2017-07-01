[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DND.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DND.App_Start.NinjectWebCommon), "Stop")]

namespace DND.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNet.Identity;
    using Core.Model;
    using Core.Interfaces.Persistance;
    using Microsoft.Owin.Security;

    using Microsoft.AspNet.Identity.EntityFramework;
    using AutoMapper;
    using AutoMapper.Configuration;
    using Solution.Base.Automapper;
    using AutoMapper.QueryableExtensions;
     using Ninject.Web.Mvc.FilterBindingSyntax;
    using Solution.Base.Implementation.Model;
    using Solution.Base.Filters;
    using System.Web.Mvc;
    using Solution.Base.Ninject;
    using Ninject.Modules;
    using Solution.Base.Tasks;
    using System.Collections.Generic;
    using System.Security.Principal;
    using Solution.Base.ModelMetadata;
    using Solution.Base.Interfaces.Persistance;

    // Ninject - Ninject core dll
    //Ninject.Web.Common - Common Web functionality for Ninject eg. InRequestScope()
    //Ninject.MVC5 - MVC dependency injectors eg. to provide Controllers for MVC
    //Ninject.Web.Common.WebHost - Registers the dependency injectors from Ninject.MVC5 when IIS starts the web app.If you are not using IIS you will need a different package, check above
    //Ninject.Web.WebApi WebApi dependency injectors eg. to provide Controllers for WebApi
    //Ninject.web.WebApi.WebHost - Registers the dependency injectors from Ninject.Web.WebApi when IIS starts the web app.

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var appName = "DND";
            var kernel = NinjectConfig.BuildKernel<User>(appName);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<IBaseDbContext>().To<IApplicationDbContext>();
           // //https://visualstudiomagazine.com/articles/2014/10/01/binding-by-convention.aspx
           //  string pluginsPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), "plugins\\");
           //if (!Directory.Exists(pluginsPath)) Directory.CreateDirectory(pluginsPath);

           // Predicate<Assembly> filterPredictate = (a => a.FullName.Contains("DND") || a.FullName.Contains("Solution"));
           //Func<Assembly, Boolean> filterFunc = (a => a.FullName.Contains("DND") || a.FullName.Contains("Solution"));

            

           // //kernel.Bind(x => x
           // //   .FromAssembliesInPath(pluginsPath, filterPredictate)
           // //  .SelectAllClasses()
           // //   .BindDefaultInterface());

           // //kernel.Bind(x => x
           // //.FromAssembliesInPath(pluginsPath, filterPredictate)
           // //.SelectAllClasses()
           // //.BindAllInterfaces());

           // //Events
           // var types = new List<Type>();
           // types.Add(typeof(IRunAtInit));
           // types.Add(typeof(IRunAtStartup));
           // types.Add(typeof(IRunOnEachRequest));
           // types.Add(typeof(IRunOnError));
           // types.Add(typeof(IRunAfterEachRequest));

           // kernel.Bind(x => x
           //  .FromAssembliesInPath(pluginsPath, filterPredictate)
           //  .SelectAllClasses().InheritedFromAny(types)
           // .BindAllInterfaces());

           //// kernel.Load(new NinjectAutomapperModule("DND"));

           // //kernel.Bind<ModelMetadataProvider>().To<ExtensibleModelMetadataProvider>();

           // //Will only bind to referenced assemblies
           // //kernel.Bind(x => x.FromAssembliesMatching("DND.*.dll")
           // //   .SelectAllClasses()
           // // .BindDefaultInterface()
           // //  );

           // //https://visualstudiomagazine.com/articles/2014/10/01/binding-by-convention.aspx
           // //string pluginsPath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), "plugins\\");
           // //if (!Directory.Exists(pluginsPath)) Directory.CreateDirectory(pluginsPath);

           // //Predicate<Assembly> filterPredictate = (a => a.FullName.Contains(_name) || a.FullName.Contains("Solution"));
           // //Func<Assembly, Boolean> filterFunc = (a => a.FullName.Contains(_name) || a.FullName.Contains("Solution"));

           // kernel.Bind(x => x
           //   .FromAssembliesInPath(pluginsPath, filterPredictate)
           //   .SelectAllClasses().InheritedFrom<IModelMetadataFilter>()
           //   .BindAllInterfaces());

        }
    }
}
