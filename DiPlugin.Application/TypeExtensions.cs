using System;

namespace DiPlugin.Application
{
    public static class TypeExtensions
    {
        public static string GetAssemblyName(this Type type)
        {
            return type.Assembly.GetName().Name;
        }
    }
}
