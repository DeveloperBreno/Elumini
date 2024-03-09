using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Domain.ViewModels
{
    public class TarefaViewModel
    {
        public int id { get; set; }
        public string texto { get; set; }
        public string status { get; set; }
        public DateTime data { get; set; }
    }
}
