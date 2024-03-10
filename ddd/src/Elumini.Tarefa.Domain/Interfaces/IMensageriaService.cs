namespace Elumini.Tarefa.Domain.Interfaces
{
    // define o nome das filas
    public enum MensageriaQueue
    {
        CriarOrEditarTarefa,
    }

    public interface IMensageriaService
    {
        Task<bool> InserirOuAtualizar(byte[] body, MensageriaQueue queueName);

        byte[] SerializeObjectToBytes(object obj);

        string ObterMensagemAssync(MensageriaQueue queueName);
    }
}