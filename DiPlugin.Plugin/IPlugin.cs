using DiPlugin.DependencyInjection;

namespace DiPlugin.Plugin
{
    public interface IPlugin
    {
        string Name { get; }

        void Configure(IDependencyRegister dependencyRegister);
    }
}
