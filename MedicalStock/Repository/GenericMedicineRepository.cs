using AutoMapper;
using MedicalStock.Data;
using MedicalStock.Model;
using MedicalStock.Model.Dtos;
using MedicalStock.Repository.IRepository;

namespace MedicalStock.Repository
{
    public class GenericMedicineRepository : IGenericMedicineRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenericMedicineRepository(DataContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public GenericMedicineDto CreateGenericMedicine(GenericMedicineDto genericMedicineDto)
        {
            var data = new GenericMedicine
            {
                MedicineId = genericMedicineDto.Medicines.Id,
                GenericId = genericMedicineDto.Generics.Id
            };

            _context.GenericMedications.Add(data);
            _context.SaveChanges();
            return genericMedicineDto;
        }

        public void DeleteByMedicineId(int medicineId)
        {
            var data = _context.GenericMedications.Where(gm => gm.MedicineId == medicineId).ToList();
            if (data.Any())
            {
                _context.GenericMedications.RemoveRange(data);
                _context.SaveChanges();
            }
        }

        public GenericMedicineDto DeleteGenericMedicine(GenericMedicineDto genericMedicineDto)
        {
            var data = _mapper.Map<GenericMedicine>(genericMedicineDto);
            data.GenericMedicationId = genericMedicineDto.Id;
            _context.GenericMedications.Remove(data);
            _context.SaveChanges();
            return genericMedicineDto;
        }

        public bool IsGenericInUse(int genericId)
        {
            return _context.GenericMedications.Any(gm => gm.GenericId == genericId);
        }
    }
}
