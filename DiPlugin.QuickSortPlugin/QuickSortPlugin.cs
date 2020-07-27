using DiPlugin.Common;
using DiPlugin.DependencyInjection;
using DiPlugin.Plugin;

namespace DiPlugin.QuickSortPlugin
{
    public class QuickSortPlugin: IPlugin
    {
        public string Name { get; } = "Quick sort plugin";

        public void Configure(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddSingleton<ISortService, QuickSortService>();
        }
    }
}
