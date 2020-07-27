using System;

namespace DiPlugin.Common
{
    public interface ISortService
    {
        T[] Sort<T>(T[] array) where T : IComparable<T>;
    }
}
