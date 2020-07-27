using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimplePlugin.Plugin;

namespace SimplePlugin.Application
{
    public static class PluginLoader
    {
        private const string PluginFolderName = "Plugins";

        public static IReadOnlyList<ISortablePlugin> Load(string directory)
        {
            var assemblies = AssemblyLoader.Load(Path.Combine(directory, PluginFolderName));

            return Load(assemblies);
        }

        private static IReadOnlyList<ISortablePlugin> Load(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(assembly => assembly.CreateInstances<ISortablePlugin>())
                .ToList();
        }
    }
}
