using Elumini.Tarefa.Domain.Entites;
using Elumini.Tarefa.Domain.Interfaces;
using Elumini.Tarefa.Domain.ViewModels;
using Elumini.Tarefa.Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Infraestrutura.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly EluminiDbContext _dbContext;

        public TarefaRepository()
        {
            _dbContext = new EluminiDbContext();
        }
        public TarefaRepository(EluminiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Domain.Entites.Tarefa[] Get()
        {
            return _dbContext.Tarefas
                .Include(o => o.ParametroStatus)
                .Include(o => o.Observacao).ToArray();
        }

        public async Task Inserir(Domain.Entites.Tarefa tarefa)
        {
            _dbContext.Tarefas.Add(tarefa);
            await _dbContext.SaveChangesAsync();
        }

    }
}
