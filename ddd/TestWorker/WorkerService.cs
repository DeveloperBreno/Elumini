using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging; // Adicionado para suporte a logging

public class WorkerService
{
    private readonly IMensageriaService _mensageriaService;
    private readonly ITarefaService _tarefaService;
    private readonly ILogger<WorkerService> _logger; // Adicionado logger

    public WorkerService(
        IMensageriaService mensageriaService,
        ITarefaService tarefaService,
         ILogger<WorkerService> logger
        ) // Adicionado logger no construtor
    {
        _mensageriaService = mensageriaService;
        _tarefaService = tarefaService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        // Lógica do loop principal do worker
        while (true)
        {
            try
            {
                // Obtenha e processe mensagens da fila
                var mensagem = _mensageriaService.ObterMensagemAssync(MensageriaQueue.CriarOrEditarTarefa);

                // Execute a lógica de processamento da mensagem
                
                if (!string.IsNullOrWhiteSpace(mensagem))
                {
                    var tarefaViewModel = JsonConvert.DeserializeObject<TarefaViewModel>(mensagem);

                    bool ok = _tarefaService.InserirOuAtualizar(tarefaViewModel);
                    if (ok)
                    {
                        _logger.LogInformation("Tarefa processada com sucesso.");
                    }
                    else
                    {
                        _logger.LogError("Erro ao processar a tarefa.");
                    }
                }

                // Aguarde um intervalo antes de verificar novamente a fila
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                // Lidar com exceções, logar erros, etc.
                _logger.LogError(ex, $"Erro no worker. Message: {ex.Message}, StackTrace: {ex.StackTrace}");

            }
        }
    }
}
