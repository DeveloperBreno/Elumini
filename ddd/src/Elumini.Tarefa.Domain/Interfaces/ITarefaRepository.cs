
using Elumini.Tarefa.Domain.ViewModels;

namespace Elumini.Tarefa.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task Inserir(Domain.Entites.Tarefa tarefa);

        Entites.Tarefa[] Get();
    }
}
