using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalStock.Model
{
    public class GenericMedicine
    {
        [Key]
        public int GenericMedicationId { get; set; }

        [Required]
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicines { get; set; }

        [Required]
        [ForeignKey("Generic")]
        public int GenericId { get; set; }
        public Generic Generics { get; set; }
    }
}
