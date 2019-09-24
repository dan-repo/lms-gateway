using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace LmsGateway.Core.Infrastructure
{
    public interface ITypeFinder
    {
        List<Assembly> GetAssemblies();
        List<TypeInfo> FindClassesOfType<T>();
        List<TypeInfo> FindClassesOfType<T>(List<Assembly> assemblies);

    }
}
