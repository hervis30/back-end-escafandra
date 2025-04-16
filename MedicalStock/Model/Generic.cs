using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalStock.Model
{
    public class Generic
    {
        [Key]
        public int GenericId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }

    }
}
