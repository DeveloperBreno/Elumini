using Elumini.Tarefa.Domain.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging; 

public class WorkerService
{
    private readonly IMensageriaService _mensageriaService;
    private readonly ITarefaService _tarefaService;
    private readonly ILogger<WorkerService> _logger; 

    public WorkerService(
        IMensageriaService mensageriaService,
        ITarefaService tarefaService,
         ILogger<WorkerService> logger
        ) 
    {
        _mensageriaService = mensageriaService;
        _tarefaService = tarefaService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            try
            {
                var mensagem = _mensageriaService.ObterMensagemAssync(MensageriaQueue.DeletarTarefa);
                if (!string.IsNullOrWhiteSpace(mensagem))
                {
                    var tarefaId = JsonConvert.DeserializeObject<int>(mensagem);
                    bool ok = _tarefaService.DeletarPorId(tarefaId);
                    if (ok)
                    {
                        _logger.LogInformation($"Tarefa id {tarefaId} excluída com sucesso.");
                    }
                    else
                    {
                        _logger.LogError($"Erro ao excluir tarefa com id {tarefaId}.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, logar erros, etc.
                _logger.LogError(ex, $"Erro no worker. Message: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }
    }
}
