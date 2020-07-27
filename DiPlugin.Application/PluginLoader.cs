using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DiPlugin.Plugin;

namespace DiPlugin.Application
{
    public static class PluginLoader
    {
        private const string PluginFolderName = "Plugins";

        public static IReadOnlyList<IPlugin> Load(string directory)
        {
            var assemblies = AssemblyLoader.Load(Path.Combine(directory, PluginFolderName));

            return Load(assemblies);
        }

        private static IReadOnlyList<IPlugin> Load(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(assembly => assembly.CreateInstances<IPlugin>())
                .ToList();
        }
    }
}
