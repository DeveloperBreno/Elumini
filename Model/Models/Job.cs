using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    /// <summary>
    /// Job é a tabela que representa  a tarefa
    /// </summary>
    public class Job
    {
        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// status id | required
        /// </summary>
        [Column("StatusId", TypeName = "int")]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "This field is required")]
        public int StatusId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "Status")]
        [ForeignKey("StatusId")]
        public virtual Parameters? ParameterStatus { get; set; }


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
        public virtual Text? Text { get; set; }

        /// <summary>
	/// data
	/// </summary>
	[Column("Date", TypeName = "datetime")]
        public DateTime Date { get; set; }

    }
}
