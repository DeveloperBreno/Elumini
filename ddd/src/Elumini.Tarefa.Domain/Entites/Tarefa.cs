using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Elumini.Tarefa.Domain.ViewModels;

namespace Elumini.Tarefa.Domain.Entites
{
    public class Tarefa : Entity
    {
        /// <summary>
        /// status id | required
        /// </summary>
        [Column("StatusId", TypeName = "int")]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "This field is required")]
        public int StatusId { get;  set; }

        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "Status")]
        [ForeignKey("StatusId")]
        public virtual Parametro ParametroStatus { get;  set; }

        /// <summary>
        /// Text id | required
        /// </summary>
        [Column("TextId", TypeName = "int")]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "This field is required")]
        public int TextId { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [Display(Name = "Status")]
        [ForeignKey("TextId")]
        public virtual Observacao Observacao { get; set; }

        /// <summary>
		/// data
		/// </summary>
		[Column("Date", TypeName = "datetime")]
        public DateTime Date { get; set; }

        public Tarefa() { }


    }
}
