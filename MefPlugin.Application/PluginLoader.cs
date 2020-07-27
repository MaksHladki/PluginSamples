using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using MefPlugin.Plugin;

namespace MefPlugin.Application
{
    public class PluginLoader : IDisposable
    {
        public static IEnumerable<Lazy<TPlugin, TPluginMetadata>> LoadLazyWithMetadata<TPlugin, TPluginMetadata>(Assembly assembly)
        {
            if (assembly == null)
            {
                return Enumerable.Empty<Lazy<TPlugin, TPluginMetadata>>();
            }

            var catalog = new AssemblyCatalog(assembly);
            var container = new CompositionContainer(catalog);

            return container.GetExports<TPlugin, TPluginMetadata>();
        }

        private const string PluginFolderName = "Plugins";
        private const string PluginSearchPattern = "*.dll";

        private readonly CompositionContainer _container;
        private readonly DirectoryCatalog _catalog;
        private readonly FileSystemWatcher _fileWatcher;

        [ImportMany]
        public List<Lazy<ISortablePlugin>> Plugins { get; set; }

        [ImportMany]
        public List<Lazy<ISortablePlugin, IPluginMetadata>> PluginsWithMetadata { get; set; }

        public event EventHandler<ExportsChangeEventArgs> ExportsChanged;

        public PluginLoader(string directory)
        {
            var pluginDirectory = Path.Combine(directory, PluginFolderName);

            _fileWatcher = new FileSystemWatcher(pluginDirectory, PluginSearchPattern);
            _fileWatcher.Created += OnPluginFileCreatedOrDeleted;
            _fileWatcher.Deleted += OnPluginFileCreatedOrDeleted;

            _catalog = new DirectoryCatalog(pluginDirectory, PluginSearchPattern);
            _container = new CompositionContainer(_catalog);
            _container.ExportsChanged += OnContainerExportsChanged;

            LoadPlugins();
        }

        public void Dispose()
        {
            _fileWatcher?.Dispose();
            _container?.Dispose();
            _catalog?.Dispose();
        }

        private void OnContainerExportsChanged(object sender, ExportsChangeEventArgs e)
        {
            ExportsChanged?.Invoke(this, e);
        }

        private void OnPluginFileCreatedOrDeleted(object sender, FileSystemEventArgs e)
        {
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            try
            {
                _catalog.Refresh();
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
