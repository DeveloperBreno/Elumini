
using Elumini.Tarefa.Domain.ViewModels;

namespace Elumini.Tarefa.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task<bool> InserirOrAtualizarAssync(TarefaViewModel tarefaViewModel);

        Entites.Tarefa[] Get();
        Entites.Tarefa? GetById(int id);
    }
}
