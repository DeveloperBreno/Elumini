using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elumini.Tarefa.Domain.Entites
{
    public class Parametro : Entity
    {
        /// <summary>
        /// Key
        /// </summary>
        [Required(ErrorMessage = "This field is required")]
        [Column("Chave", TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string Chave { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [Required(ErrorMessage = "This field is required")]
        [Column("Valor", TypeName = "varchar(200)")]
        [MaxLength(200)]
        public string Valor { get; set; }

        /// <summary>
        /// order
        /// </summary>
        [Required(ErrorMessage = "This field is required")]
        [Column("Order", TypeName = "int")]
        public int Ordem { get; set; }
    }
}
