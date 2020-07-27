using DiPlugin.Common;
using Microsoft.Extensions.Logging;
using System;

namespace DiPlugin.QuickSortPlugin
{
    public class QuickSortService : ISortService
    {
        private readonly ILogger<QuickSortService> _logger;

        public QuickSortService(ILogger<QuickSortService> logger)
        {
            _logger = logger;
        }

        public T[] Sort<T>(T[] array) where T : IComparable<T>
        {
            _logger.LogInformation("Quick sort algorithm");

            return Sort(array, 0, array.Length - 1);
        }

        private T[] Sort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            if (left >= right)
            {
                return array;
            }

            var c = GetPartition(array, left, right);

            Sort(array, left, c - 1);
            Sort(array, c + 1, right);

            return array;
        }

        private int GetPartition<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            var i = left;

            for (var j = left; j <= right; j++)
            {
                if (array[j].CompareTo(array[right]) > 0)
                {
                    continue;
                }

                T t = array[i];
                array[i] = array[j];
                array[j] = t;

                i++;
            }

            return i - 1;
        }
    }
}
