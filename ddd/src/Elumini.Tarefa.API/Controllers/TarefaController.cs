using Elumini.Tarefa.Domain.Entites;
using Elumini.Tarefa.Domain.Interfaces;
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
        public IActionResult Get() => Ok(_tarefaService.Get());

        [HttpPost(Name = "tarefa")]
        public IActionResult Criar(Domain.Entites.Tarefa tarefa)
        {
            _= _tarefaService.Inserir(tarefa);
            return Ok();
        }
    }
}