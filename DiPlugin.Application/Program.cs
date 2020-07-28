using System;
using System.Linq;
using DiPlugin.Common;
using DiPlugin.Plugin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiPlugin.Application
{
    public class Program
    {
        private static readonly int[] Data = { 2, 7, 0, 11, -4, 3, 7 };

        private static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddPlugins(AppDomain.CurrentDomain.BaseDirectory);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });

            var pluginDictionary = serviceProvider
                .GetServices<IPlugin>()
                .ToDictionary(k => k.GetType().GetAssemblyName(), v => v);

            var sortServiceDictionary = serviceProvider
                .GetServices<ISortService>()
                .ToDictionary(k => k.GetType().GetAssemblyName(), v => v);

            foreach ((string assemblyName, IPlugin plugin) in pluginDictionary)
            {
                ISortService sortService = sortServiceDictionary[assemblyName];
                var sortedArray = sortService.Sort((int[])Data.Clone());

                Console.WriteLine($"Name: {plugin.Name}");
                Console.WriteLine($"Results: {string.Join(", ", sortedArray)}");
            }
        }
    }
}
