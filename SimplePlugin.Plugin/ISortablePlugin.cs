using System;

namespace SimplePlugin.Plugin
{
    public interface ISortablePlugin
    {
        string Name { get; }
        T[] Sort<T>(T[] array) where T : IComparable<T>;
    }
}
