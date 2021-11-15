using Games.Application.Services;
using Games.Domain.Contracts.Repositories;
using Games.Domain.Contracts.Services;
using Games.Domain.Contracts.UoW;
using Games.Domain.Shared.Contexts;
using Games.Infra.Data.Repositories;
using Games.Infra.Data.Session;
using Games.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Games.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<DbSession>();
            services.AddScoped<NotificationContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IGameRepository, GameRepository>();
            
        }
    }
}