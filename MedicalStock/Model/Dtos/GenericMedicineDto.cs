
namespace MedicalStock.Model.Dtos
{
    public class GenericMedicineDto
    {
        public int Id { get; set; }

        public MedicineDto Medicines { get; set; }

        public GenericDto Generics { get; set; }
    }
}
