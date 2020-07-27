using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimplePlugin.Application
{
    public static class AssemblyExtensions
    {
        public static IReadOnlyList<T> CreateInstances<T>(this Assembly assembly) where T : class
        {
            return assembly
                .GetExportedTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => (T)Activator.CreateInstance(type))
                .ToList();
        }
    }
}
