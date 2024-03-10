using Elumini.Tarefa.Domain.Entites;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;

namespace Elumini.Tarefa.Application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public Domain.Entites.Tarefa[] Get()
        {
            return _tarefaRepository.Get();
        }

        public Domain.Entites.Tarefa? GetById(int id)
        {
            return _tarefaRepository.GetById(id);
        }

        public bool InserirOuAtualizar(TarefaViewModel tarefaViewModel)
        {
            var ok = _tarefaRepository.InserirOrAtualizarAssync(tarefaViewModel).Result;
            return ok;
        }
    }
}
