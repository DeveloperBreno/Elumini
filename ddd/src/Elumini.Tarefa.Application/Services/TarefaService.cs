using Elumini.Tarefa.Domain.Interfaces;

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

        public async Task<bool> Inserir(Domain.Entites.Tarefa tarefa)
        {
            // validacao

            // insercao
            await _tarefaRepository.Inserir(tarefa);
            return true;
        }

    }
}
