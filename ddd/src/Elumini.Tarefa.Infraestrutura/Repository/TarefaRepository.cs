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

        public Domain.Entites.Tarefa? GetById(int id)
        {
            return _dbContext.Tarefas
                .Include(o => o.ParametroStatus)
                .Include(o => o.Observacao).FirstOrDefault(o => o.Id.Equals(id));
        }

        public async Task Inserir(Domain.Entites.Tarefa tarefa)
        {
            _dbContext.Tarefas.Add(tarefa);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> InserirOrAtualizarAssync(TarefaViewModel tarefaViewModel)
        {

            var status = _dbContext.Parametros.FirstOrDefault(o => o.Valor.Equals(tarefaViewModel.status)) ?? throw new Exception("Parametro status não encontrado");

            if (tarefaViewModel.id > 0)
            {
                // atualizando
                var tarefa = GetById(tarefaViewModel.id);

                if (tarefa is null)
                {
                    return Task.FromResult(false);
                }

                tarefa.Date = tarefaViewModel.data;
                tarefa.StatusId = status.Id;
                tarefa.Observacao.Texto = tarefaViewModel.texto;

                _dbContext.Tarefas.Update(tarefa);
                _dbContext.SaveChanges();

            }
            else
            {
                // criando

                var tarefa = new Domain.Entites.Tarefa()
                {
                    ParametroStatus = status,
                    Date = tarefaViewModel.data,
                    Observacao = new Observacao()
                    {
                        Texto = tarefaViewModel.texto
                    }
                };
                _dbContext.Tarefas.Add(tarefa);
                _dbContext.SaveChanges();
            }

            return Task.FromResult(true);

        }
    }
}
