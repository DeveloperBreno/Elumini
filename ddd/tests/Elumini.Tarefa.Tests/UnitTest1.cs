using Elumini.Tarefa.Application.Services;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Elumini.Tarefa.Infraestrutura.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Elumini.Tarefa.Tests
{
    public class UnitTest1
    {

        private readonly IMensageriaService _mensageriaService;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly ITarefaService _tarefaService;

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configuração da injeção de dependência aqui
            services.AddTransient<IMensageriaService, MensageriaService>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<ITarefaService, TarefaService>();

            // Adicione outros serviços e configurações necessários

            return services.BuildServiceProvider();
        }

        public UnitTest1()
        {
            var serviceProvider = ConfigureServices();
            _mensageriaService = serviceProvider.GetRequiredService<IMensageriaService>();
            _tarefaRepository = serviceProvider.GetRequiredService<ITarefaRepository>();
            _tarefaService = serviceProvider.GetRequiredService<ITarefaService>();
        }

        [Fact]
        public void InserirNaFila()
        {
            var tarefaViewModel = new TarefaViewModel()
            {
                id = 0,
                texto = "teste",
                status = "To do",
                data = DateTime.Now
            };

            var body = _mensageriaService.SerializeObjectToBytes(tarefaViewModel);
            var result = _mensageriaService.InserirOuAtualizar(body, MensageriaQueue.CriarOrEditarTarefa).Result;

            Assert.Equal(true, result);
        }

        [Fact]
        public void InserirNoBnaco()
        {
            var tarefaViewModel = new TarefaViewModel()
            {
                id = 0,
                texto = "teste",
                status = "To do",
                data = DateTime.Now
            };

            var result = _tarefaService.InserirOuAtualizar(tarefaViewModel);

            Assert.Equal(true, result);
        }


        [Fact]
        public void LerDaTarefas()
        {
            var tarefas = _tarefaRepository.Get();
            Assert.True(tarefas.Length > 0);
        }
    }
}