using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Elumini.Tarefa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        private readonly IMensageriaService _mensageriaService;

        private readonly ILogger<TarefaController> _logger;

        public TarefaController(
            ILogger<TarefaController> logger,
            ITarefaService produtoService,
            IMensageriaService mensageriaService)
        {
            _logger = logger;
            _tarefaService = produtoService;
            _mensageriaService = mensageriaService;
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

            return Ok(new { result = tarefas });
        }

        [HttpGet("{id}", Name = "tarefaPorId")]
        public IActionResult GetById(int id)
        {
            var tarefa = _tarefaService.GetById(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            var tarefaViewModel = new TarefaViewModel()
            {
                id = tarefa.Id,
                texto = tarefa.Observacao.Texto,
                status = tarefa.ParametroStatus.Valor,
                data = tarefa.Date
            };

            return Ok(new
            {
                result = tarefaViewModel
            });
        }

        [HttpPost(Name = "tarefa")]
        public IActionResult Criar(TarefaViewModel tarefaViewModel)
        {
            byte[] body = _mensageriaService.SerializeObjectToBytes(tarefaViewModel);
            var ok = _mensageriaService.InserirOuAtualizar(body, MensageriaQueue.CriarOrEditarTarefa).Result;

            if (ok)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpPut(Name = "tarefa")]
        public IActionResult Atualizar(TarefaViewModel tarefaViewModel)
        {
            byte[] body = _mensageriaService.SerializeObjectToBytes(tarefaViewModel);
            var ok = _mensageriaService.InserirOuAtualizar(body, MensageriaQueue.CriarOrEditarTarefa).Result;

            if (ok)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}