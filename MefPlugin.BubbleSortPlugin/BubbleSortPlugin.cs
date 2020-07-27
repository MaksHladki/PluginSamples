using System;
using System.ComponentModel.Composition;
using MefPlugin.Plugin;

namespace MefPlugin.BubbleSortPlugin
{
    [Export(typeof(ISortablePlugin))]
    [ExportMetadata("Name", "Bubble sort plugin")]
    [ExportMetadata("Priority", 1)]
    public class BubbleSortPlugin : ISortablePlugin
    {
        public T[] Sort<T>(T[] array) where T : IComparable<T>
        {
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
