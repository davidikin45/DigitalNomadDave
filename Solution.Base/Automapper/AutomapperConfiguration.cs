﻿using AutoMapper;
using Solution.Base.Interfaces.Automapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Automapper
{
    public class AutoMapperConfiguration
    {
        public AutoMapperConfiguration(IMapperConfigurationExpression mapperConfiguration, Func<Assembly, bool> assemblyFilter = null)
        {
            var target = Assembly.GetCallingAssembly();
            Configure(mapperConfiguration, assemblyFilter);
        }
        public void Configure(IMapperConfigurationExpression mapperConfiguration, Func<Assembly, bool> assemblyFilter = null)
        {
            //mapperConfiguration.AddProfile(new UserProfileMapping(mapperConfiguration));
            RegisterMappings(mapperConfiguration, assemblyFilter);
        }

        public static void RegisterMappings(IMapperConfigurationExpression cfg, Func<Assembly, bool> assemblyFilter = null)
        {

            Func<Assembly, bool> loadAllFilter = (x => true);

            var assembliesToLoad = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assemblyFilter ?? loadAllFilter)
                .Select(a => Assembly.Load(a.GetName()))
                .ToList();

            LoadMapsFromAssemblies(cfg, assembliesToLoad.ToArray());
        }

        public static void LoadMapsFromAssemblies(IMapperConfigurationExpression cfg, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToArray();

            Load(cfg, types);
        }

        private static void Load(IMapperConfigurationExpression cfg, Type[] types)
        {
            LoadIMapFromMappings(cfg, types);
            LoadIMapToMappings(cfg, types);

            LoadCustomMappings(cfg, types);
        }

        private static void LoadCustomMappings(IMapperConfigurationExpression cfg, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(cfg);
            }
        }

        private static void LoadIMapFromMappings(IMapperConfigurationExpression cfg, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                cfg.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadIMapToMappings(IMapperConfigurationExpression cfg, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments()[0],
                            Source = t
                        }).ToArray();

            foreach (var map in maps)
            {
                cfg.CreateMap(map.Source, map.Destination);
            }
        }
    }

}
