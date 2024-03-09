namespace Elumini.Tarefa.Domain.Interfaces
{
    // define o nome das filas
    public enum MensageriaQueue
    {
        CriarOrEditarTarefa,
    }

    public interface IMensageriaService
    {
        Task<bool> Inserir(byte[] body, MensageriaQueue queueName);

        byte[] SerializeObjectToBytes(object obj);
    }
}