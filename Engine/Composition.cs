using Engine.Services;
using Engine.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Engine
{
    public static class Composition
    {
        public static IServiceCollection AddEngine(this IServiceCollection services)
        {
            services.AddSingleton<IEngine, EngineService>();
            services.AddSingleton<ItemRepository>();
            return services;
        }
    }
}