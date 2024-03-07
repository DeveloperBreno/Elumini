using Elumini.Tarefa.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task Inserir(Produto produto);
    }
}
