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
        public int StatusId { get; private set; }

        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "Status")]
        [ForeignKey("StatusId")]
        public virtual Parametro ParametroStatus { get; private set; }

        /// <summary>
        /// Text id | required
        /// </summary>
        [Column("TextId", TypeName = "int")]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "This field is required")]
        public int TextId { get; private set; }

        /// <summary>
        /// Text
        /// </summary>
        [Display(Name = "Status")]
        [ForeignKey("TextId")]
        public virtual Observacao Observacao { get; private set; }

        /// <summary>
		/// data
		/// </summary>
		[Column("Date", TypeName = "datetime")]
        public DateTime Date { get; private set; }

        public Tarefa() { }

        public Tarefa(int statusId, int textoId, DateTime data ) {
            this.StatusId = statusId;
            this.TextId = textoId;
            this.Date = data;
        }

    }
}
