﻿using Elumini.Tarefa.Application.Services;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Infraestrutura.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;

using Elumini.Tarefa.Application.Services;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Infraestrutura.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

public class Program
{

    static async Task Main(string[] args)
    {
        // Configuração do DI (injeção de dependência)
        var serviceProvider = ConfigureServices();

        // Obtém uma instância do serviço do worker
        var workerService = serviceProvider.GetRequiredService<WorkerService>();

        // Executa o loop principal do worker
        await workerService.RunAsync();
    }

    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Configuração da injeção de dependência aqui
        services.AddTransient<IMensageriaService, MensageriaService>();
        services.AddTransient<ITarefaService, TarefaService>();
        services.AddTransient<ITarefaRepository, TarefaRepository>();

        // Configuração do logger para o WorkerService
        services.AddTransient<ILogger<WorkerService>>(provider =>
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // ou qualquer outro provedor desejado
            });

            return loggerFactory.CreateLogger<WorkerService>();
        });

        services.AddTransient<WorkerService>();

        // Adicione outros serviços e configurações necessários

        return services.BuildServiceProvider();
    }
}