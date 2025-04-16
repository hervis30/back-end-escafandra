
namespace MedicalStock.Model.Dtos
{
    public class MedicineDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public List<GenericDto> Generics { get; set; } 
    }
}
