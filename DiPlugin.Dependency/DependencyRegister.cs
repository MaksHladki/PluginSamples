using Microsoft.Extensions.DependencyInjection;

namespace DiPlugin.DependencyInjection
{
    public class DependencyRegister : IDependencyRegister
    {
        private readonly IServiceCollection _serviceCollection;

        public DependencyRegister(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void AddScoped<TService>() where TService : class
        {
            _serviceCollection.AddScoped<TService>();
        }

        public void AddScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddScoped<TService, TImplementation>();
        }

        public void AddSingleton<TService>() where TService : class
        {
            _serviceCollection.AddSingleton<TService>();
        }

        public void AddSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddTransient<TService>() where TService : class
        {
            _serviceCollection.AddTransient<TService>();
        }

        public void AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public void AddTransientForMultiImplementation<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddTransient<TImplementation>()
                .AddTransient<TService, TImplementation>(s => s.GetService<TImplementation>());
        }

        public void AddScopedForMultiImplementation<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddScoped<TImplementation>()
                .AddScoped<TService, TImplementation>(s => s.GetService<TImplementation>());
        }
    }
}
