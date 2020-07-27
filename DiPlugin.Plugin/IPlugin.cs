using DiPlugin.DependencyInjection;

namespace DiPlugin.Plugin
{
    public interface IPlugin
    {
        public string Name { get; }

        void Configure(IDependencyRegister dependencyRegister);
    }
}
