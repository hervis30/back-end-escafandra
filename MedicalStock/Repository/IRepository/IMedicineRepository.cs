using MedicalStock.Model.Dtos;

namespace MedicalStock.Repository.IRepository
{
    public interface IMedicineRepository
    {
        MedicineDto CreateMedicine(MedicineDto medicine);
        MedicineDto UpdateMedicine(MedicineDto medicine);
        MedicineDto DeleteMedicine(MedicineDto medicine);
        List<MedicineDto> ListAll();
        MedicineDto GetMedicineById(int id);
    }
}
