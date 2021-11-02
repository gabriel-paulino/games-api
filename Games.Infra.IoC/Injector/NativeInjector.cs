using Games.Application.Services;
using Games.Domain.Contracts.Repositories;
using Games.Domain.Contracts.Services;
using Games.Domain.Contracts.UoW;
using Games.Infra.Data.Repositories;
using Games.Infra.Data.Session;
using Games.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Games.Infra.IoC.Injector
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IGameRepository, GameRepository>();
        }
    }
}