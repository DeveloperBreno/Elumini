using Elumini.Tarefa.Application.Services;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Elumini.Tarefa.Infraestrutura.Repository;
using Microsoft.Extensions.Configuration;
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

            var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

            services.AddTransient<IMensageriaService>(provider =>
            {
                return new MensageriaService(configuration);
            });


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
            var result = _mensageriaService.InserirNaFila(body, MensageriaQueue.CriarOrEditarTarefa).Result;

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
        public void LerTarefas()
        {
            var tarefas = _tarefaService.Get();
            Assert.True(tarefas.Length > 0);
        }

        [Fact]
        public void LerTarefasPorRepository()
        {
            var tarefas = _tarefaRepository.Get();
            Assert.True(tarefas.Length > 0);
        }

        [Fact]
        public void LerUmaTarefaAleatoria()
        {
            var tarefas = _tarefaService.Get();
            var random = new Random();
            var indiceAleatorio = random.Next(0, tarefas.Length); // Gera um índice aleatório dentro do intervalo válido
            var tarefaAleatoria = tarefas[indiceAleatorio]; // Seleciona a tarefa correspondente ao índice aleatório
            var tarefaDoBanco = _tarefaService.GetById(tarefaAleatoria.Id);
            Assert.True(tarefaDoBanco is not null);
        }

        [Fact]
        public void LerDaFila()
        {
            var obj = _mensageriaService.ObterMensagemAssync(MensageriaQueue.CriarOrEditarTarefa);
            Assert.True(!string.IsNullOrWhiteSpace(obj));
        }


        [Fact]
        public void ExcluirUmaTarefaPorId()
        {
            var tarefas = _tarefaService.Get();
            var random = new Random();
            var indiceAleatorio = random.Next(0, tarefas.Length); // Gera um índice aleatório dentro do intervalo válido
            var tarefaAleatoria = tarefas[indiceAleatorio]; // Seleciona a tarefa correspondente ao índice aleatório
            var ok = _tarefaService.DeletarPorId(tarefaAleatoria.Id);
            Assert.True(ok == true);
        }

    }
}