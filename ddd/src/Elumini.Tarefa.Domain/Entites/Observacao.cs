using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Domain.Entites
{
    public class Observacao : Entity
    {
        /// <summary>
        /// Texto livre
        /// </summary>
        [Column("Words", TypeName = "varchar(500)")]
        [MinLength(0)]
        [MaxLength(500)]
        public string Texto { get;  set; } = string.Empty;
    }
}
