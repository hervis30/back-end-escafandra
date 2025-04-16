using MedicalStock.Model.Dtos;

namespace MedicalStock.Repository.IRepository
{
    public interface IGenericRepository
    {
        GenericDto CreateGeneric(GenericDto generic);
        GenericDto UpdateGeneric(GenericDto generic);
        GenericDto DeleteGeneric(GenericDto generic);
        List<GenericDto> ListAll();
        GenericDto GetGenericById(int id);
    }
}
