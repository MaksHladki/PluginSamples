using DiPlugin.DependencyInjection;
using DiPlugin.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace DiPlugin.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlugins(this IServiceCollection serviceCollection, string directory)
        {
            var dependencyRegister = new DependencyRegister(serviceCollection);
            var plugins = PluginLoader.Load(directory);

            foreach (IPlugin plugin in plugins)
            {
                serviceCollection.AddSingleton(typeof(IPlugin), plugin.GetType());
                plugin.Configure(dependencyRegister);
            }

            return serviceCollection;
        }
    }
}
