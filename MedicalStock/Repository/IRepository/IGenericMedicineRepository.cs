using MedicalStock.Model.Dtos;

namespace MedicalStock.Repository.IRepository
{
    public interface IGenericMedicineRepository
    {
        GenericMedicineDto CreateGenericMedicine(GenericMedicineDto genericMedicineDto);
        GenericMedicineDto DeleteGenericMedicine(GenericMedicineDto genericMedicineDto);
        void DeleteByMedicineId(int medicineId);
        bool IsGenericInUse(int genericId);

    }
}
