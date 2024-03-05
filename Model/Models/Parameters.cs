using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Parameters
    {
        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        [Required(ErrorMessage = "This field is required")]
        [Column("Key", TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [Required(ErrorMessage = "This field is required")]
        [Column("Value", TypeName = "varchar(200)")]
        [MaxLength(200)]
        public string Value { get; set; }

        /// <summary>
        /// order
        /// </summary>
        [Required(ErrorMessage = "This field is required")]
        [Column("Order", TypeName = "int")]
        public int Order { get; set; }

    }
}
