using Elumini.Tarefa.Application.Services;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Infraestrutura.Context;
using Elumini.Tarefa.Infraestrutura.Repository;

namespace Elumini.Tarefa.API.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolverDependecy(this IServiceCollection services)
        {
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<ITarefaService, TarefaService>();
            services.AddScoped<IMensageriaService, MensageriaService>();

            return services;    
        }
    }
}
