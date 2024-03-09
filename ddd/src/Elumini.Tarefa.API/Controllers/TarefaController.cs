using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elumini.Tarefa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        private readonly ILogger<TarefaController> _logger;

        public TarefaController(
            ILogger<TarefaController> logger,
            ITarefaService produtoService)
        {
            _logger = logger;
            _tarefaService = produtoService;
        }

        [HttpGet(Name = "tarefas")]
        public IActionResult Get()
        {
            var tarefas = _tarefaService.Get()
                .Select(t =>
                new TarefaViewModel()
                {
                    id = t.Id,
                    texto = t.Observacao.Texto,
                    status = t.ParametroStatus.Valor,
                    data = t.Date
                });

            return Ok(tarefas);
        }

        [HttpPost(Name = "tarefa")]
        public IActionResult Criar(TarefaViewModel tarefaViewModel)
        {
            // aqui chama a fila para inserir a tarefa view model
            //_tarefaFila.Inserir(tarefaViewModel)
            return Ok();
        }
    }
}