using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Text
    {

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// free text
        /// </summary>
        [Column("Words", TypeName = "varchar(500)")]
        [MinLength(0)]
        [MaxLength(500)]
        public string Words { get; set; } = string.Empty;

    }
}
