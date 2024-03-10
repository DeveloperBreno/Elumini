using Elumini.Tarefa.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Domain.Interfaces
{
    public interface ITarefaService
    {
        Entites.Tarefa[] Get();

        Entites.Tarefa? GetById(int id);

        bool InserirOuAtualizar(TarefaViewModel tarefaViewModel);
    }
}
