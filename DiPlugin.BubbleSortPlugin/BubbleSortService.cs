using System;
using DiPlugin.Common;
using Microsoft.Extensions.Logging;

namespace DiPlugin.BubbleSortPlugin
{
    public class BubbleSortService: ISortService
    {
        private readonly ILogger<BubbleSortService> _logger;

        public BubbleSortService(ILogger<BubbleSortService> logger)
        {
            _logger = logger;
        }

        public T[] Sort<T>(T[] array) where T : IComparable<T>
        {
            _logger.LogInformation("Bubble sort algorithm");

            for (var i = 1; i < array.Length; i++)
            {
                var isSorted = true;

                for (var j = 0; j < array.Length - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        isSorted = false;
                    }
                }

                if (isSorted)
                {
                    break;
                }
            }

            return array;
        }
    }
}
