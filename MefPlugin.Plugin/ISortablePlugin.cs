using System;

namespace MefPlugin.Plugin
{
    public interface ISortablePlugin
    {
        T[] Sort<T>(T[] array) where T : IComparable<T>;
    }
}
