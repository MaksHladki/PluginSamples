using System;
using SimplePlugin.Plugin;

namespace SimplePlugin.Application
{
    public class Program
    {
        private static readonly int[] Data = { 2, 7, 0, 11, -4, 3, 7 };

        private static void Main()
        {
            var plugins = PluginLoader.Load(AppDomain.CurrentDomain.BaseDirectory);

            foreach (ISortablePlugin plugin in plugins)
            {
                var sortedArray = plugin.Sort((int[])Data.Clone());

                Console.WriteLine($"Name: {plugin.Name}");
                Console.WriteLine($"Results: {string.Join(", ", sortedArray)}");
            }
        }
    }
}
