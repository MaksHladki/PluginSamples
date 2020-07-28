using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using MefPlugin.Plugin;

namespace MefPlugin.Application
{
    public class Program
    {
        private static readonly int[] Data = { 2, 7, 0, 11, -4, 3, 7 };

        private static void Main()
        {
            var pluginLoader = new PluginLoader(AppDomain.CurrentDomain.BaseDirectory);
            pluginLoader.ExportsChanged += PluginLoaderOnExportsChanged;

            var plugins = pluginLoader.Plugins;
            var pluginsWithMetadata = pluginLoader.PluginsWithMetadata;

            foreach (var pluginWithMetadata in pluginsWithMetadata)
            {
                IPluginMetadata metadata = pluginWithMetadata.Metadata;
                ISortablePlugin sortService = pluginWithMetadata.Value;
                var sortedArray = sortService.Sort((int[])Data.Clone());

                Console.WriteLine($"Name: {metadata.Name}");
                Console.WriteLine($"Results: {string.Join(", ", sortedArray)}");
            }
        }

        private static void PluginLoaderOnExportsChanged(object sender, ExportsChangeEventArgs e)
        {
            if (e.AddedExports.Any())
            {
                Console.WriteLine("Added Exports:");

                LogExportDefinitions(e.AddedExports);
            }

            if (e.RemovedExports.Any())
            {
                Console.WriteLine("Removed Exports:");

                LogExportDefinitions(e.RemovedExports);
            }
        }

        private static void LogExportDefinitions(IEnumerable<ExportDefinition> exportDefinitions)
        {
            foreach (ExportDefinition exportDefinition in exportDefinitions)
            {
                Console.WriteLine(exportDefinition.ContractName);
                Console.WriteLine(exportDefinition.Metadata);
            }
        }
    }
}
