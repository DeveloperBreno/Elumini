namespace Elumini.Tarefa.Domain.Interfaces
{
    // define o nome das filas
    public enum MensageriaQueue
    {
        CriarOrEditarTarefa,
        DeletarTarefa
    }

    public interface IMensageriaService
    {
        Task<bool> InserirNaFila(byte[] body, MensageriaQueue queueName);

        byte[] SerializeObjectToBytes(object obj);

        string ObterMensagemAssync(MensageriaQueue queueName);
    }
}