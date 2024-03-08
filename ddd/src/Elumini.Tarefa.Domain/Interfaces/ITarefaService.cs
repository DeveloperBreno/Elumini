﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Domain.Interfaces
{
    public interface ITarefaService
    {
        Task<bool> Inserir(Entites.Tarefa tarefa);

        Entites.Tarefa[] Get();
    }
}