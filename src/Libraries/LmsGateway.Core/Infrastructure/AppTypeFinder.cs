using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace LmsGateway.Core.Infrastructure
{
    public class AppTypeFinder : ITypeFinder
    {
        public List<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>()
            {
                Assembly.Load(new AssemblyName("LmsGateway.Data")),
                Assembly.Load(new AssemblyName("LmsGateway.Domain")),
                Assembly.Load(new AssemblyName("LmsGateway.Core"))
            };

            return assemblies;
        }

        public List<TypeInfo> FindClassesOfType<T>()
        {
            List<Assembly> assemblies = GetAssemblies();
            return FindClassesOfTypeHelper<T>(assemblies);
        }

        public List<TypeInfo> FindClassesOfType<T>(List<Assembly> assemblies)
        {
            return FindClassesOfTypeHelper<T>(assemblies);
        }

        private List<TypeInfo> FindClassesOfTypeHelper<T>(List<Assembly> assemblies)
        {
            return assemblies
              .SelectMany(x => x.DefinedTypes)
              .Where(dr => dr.ImplementedInterfaces.Contains(typeof(T)))
              .Select(dr => dr).ToList();
        }



        //public List<Assembly> GetAssemblies()
        //{
        //    var assemblies = new List<Assembly>();
        //    var dependencies = DependencyContext.Default.RuntimeLibraries;
        //    foreach (var library in dependencies)
        //    {
        //        if (IsCandidateCompilationLibrary(library))
        //        {
        //            var assembly = Assembly.Load(new AssemblyName(library.Name));
        //            assemblies.Add(assembly);
        //        }
        //    }

        //    return assemblies;
        //}

        //private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        //{
        //    return compilationLibrary.Name.StartsWith(nameof(CampusClassicals)) || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith(nameof(CampusClassicals)));
        //}


    }
}
