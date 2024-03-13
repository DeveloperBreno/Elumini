using Elumini.Tarefa.Domain.Entites;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Elumini.Tarefa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;
        private readonly IMensageriaService _mensageriaService;
        private readonly ILogger<TarefaController> _logger;

        private readonly Response _response;

        public TarefaController(
            ILogger<TarefaController> logger,
            ITarefaService produtoService,
            IMensageriaService mensageriaService)
        {
            _logger = logger;
            _tarefaService = produtoService;
            _mensageriaService = mensageriaService;
            _response = new Response();
        }

        [HttpGet(Name = "tarefas")]
        public IActionResult Get()
        {
            try
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

                _response.result = tarefas;
                _response.mensagem = "Tarefas recuperadas com sucesso.";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.mensagem = "Erro ao recuperar tarefas. Tente novamente mais tarde.";
                _logger.LogError(ex, $"Erro ao recuperar tarefas:\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500);
            }

        }

        [HttpGet("{id}", Name = "tarefaPorId")]
        public IActionResult GetById(int id)
        {
            try
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

                _response.result = tarefaViewModel;
                _response.mensagem = "Tarefa recuperada com sucesso.";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.mensagem = "Erro ao recuperar a tarefa.";

                _logger.LogError(ex, $"Erro ao recuperar uma tarefa por id:\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, _response);
            }
        }

        [HttpPost(Name = "tarefa")]
        public IActionResult Criar(TarefaViewModel tarefaViewModel)
        {
            try
            {
                byte[] body = _mensageriaService.SerializeObjectToBytes(tarefaViewModel);
                var ok = _mensageriaService.InserirNaFila(body, MensageriaQueue.CriarOrEditarTarefa).Result;

                if (ok)
                {
                    _response.mensagem = "Tarefa inserida na fila com sucesso.";
                    return Ok(_response);
                }

                var jsonTarefa = JsonConvert.SerializeObject(tarefaViewModel);
                _logger.LogError($"Erro ao tentar inserir uma tarefa na fila, json da tarefa: {jsonTarefa}");

                _response.mensagem = "Erro ao tentar inserir tarefa na fila, tente mais tarde";

                return StatusCode(500, _response);

            }
            catch (Exception ex)
            {
                var jsonTarefa = JsonConvert.SerializeObject(tarefaViewModel);
                _logger.LogError(ex, $"Erro ao tentar inserir uma tarefa na fila\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}\njson da tarefa: {jsonTarefa}");
                return StatusCode(500);
            }
        }

        [HttpPut(Name = "tarefa")]
        public IActionResult Atualizar(TarefaViewModel tarefaViewModel)
        {
            try
            {
                byte[] body = _mensageriaService.SerializeObjectToBytes(tarefaViewModel);
                var ok = _mensageriaService.InserirNaFila(body, MensageriaQueue.CriarOrEditarTarefa).Result;

                if (ok)
                {
                    _response.mensagem = "Tarefa inserida na fila, será atualizada em breve";
                    return Ok(_response);
                }

                var jsonTarefa = JsonConvert.SerializeObject(tarefaViewModel);
                _logger.LogError($"Erro ao tentar inserir uma tarefa na fila, json da tarefa: {jsonTarefa}");

                _response.mensagem = "Erro ao tentar atualizar a tarefa, tente mais tarde";

                return StatusCode(500, _response);
            }
            catch (Exception ex)
            {
                var jsonTarefa = JsonConvert.SerializeObject(tarefaViewModel);
                _logger.LogError(ex, $"Erro ao tentar inserir uma tarefa na fila\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}\njson da tarefa: {jsonTarefa}");

                _response.mensagem = "Erro ao tentar atualizar a tarefa, tente mais tarde";

                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}", Name = "tarefaDelete")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                byte[] body = _mensageriaService.SerializeObjectToBytes(id);
                var ok = _mensageriaService.InserirNaFila(body, MensageriaQueue.DeletarTarefa).Result;
                if (ok)
                {
                    _response.mensagem = "A tarefa será excluida em breve";
                    return Ok(_response);
                }
                else
                {
                    _logger.LogError($"Erro ao tentar inserir uma tarefa na fila: \"{MensageriaQueue.DeletarTarefa}\"\n tarefa id: {id}");

                    _response.mensagem = "Erro ao tentar deletar a tarefa, tente mais tarde";

                    return StatusCode(500, _response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar inserir uma tarefa na fila: \"{MensageriaQueue.DeletarTarefa}\"\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}\ntarefa id: {id}");

                _response.mensagem = "Erro ao tentar deletar a tarefa, tente mais tarde";

                return StatusCode(500, _response);
            }

        }
    }
}