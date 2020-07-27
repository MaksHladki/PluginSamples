using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SimplePlugin.Application
{
    public static class AssemblyLoader
    {
        private const string DefaultSearchPattern = "*.dll";

        public static IReadOnlyList<Assembly> Load(string directory, string searchPattern = DefaultSearchPattern)
        {
            var assemblies = new Dictionary<string, Assembly>();
            var files = GetFiles(directory, searchPattern);

            foreach (var file in files)
            {
                RegisterAssembly(Assembly.LoadFrom(file), assemblies);
            }

            return assemblies.Values.ToList();
        }

        private static void RegisterAssembly(Assembly assembly, IDictionary<string, Assembly> assemblies)
        {
            AssemblyName assemblyName = assembly.GetName();

            if (assemblies.TryGetValue(assemblyName.Name, out Assembly registeredAssembly))
            {
                if (assemblyName.Version > registeredAssembly.GetName().Version)
                {
                    assemblies[assemblyName.Name] = assembly;
                }
            }
            else
            {
                assemblies.Add(assemblyName.Name, assembly);
            }
        }

        private static IReadOnlyList<string> GetFiles(string directory, string searchPattern)
        {
            return Directory.GetFiles(directory, searchPattern, SearchOption.AllDirectories);
        }
    }
}
