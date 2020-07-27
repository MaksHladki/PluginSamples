using DiPlugin.Common;
using DiPlugin.DependencyInjection;
using DiPlugin.Plugin;

namespace DiPlugin.BubbleSortPlugin
{
    public class BubbleSortPlugin: IPlugin
    {
        public string Name { get; } = "Bubble sort plugin";

        public void Configure(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddSingleton<ISortService, BubbleSortService>();
        }
    }
}
