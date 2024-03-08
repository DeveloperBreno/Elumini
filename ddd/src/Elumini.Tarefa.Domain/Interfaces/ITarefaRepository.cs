
namespace Elumini.Tarefa.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task Inserir(Entites.Tarefa tarefa);

        Entites.Tarefa[] Get();
    }
}
