using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalStock.Model
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(400)")]
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
