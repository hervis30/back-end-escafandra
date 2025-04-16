using AutoMapper;
using MedicalStock.Data;
using MedicalStock.Model;
using MedicalStock.Model.Dtos;
using MedicalStock.Repository.IRepository;

namespace MedicalStock.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MedicineRepository(DataContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public MedicineDto CreateMedicine(MedicineDto medicine)
        {
            var data = _mapper.Map<Medicine>(medicine);
            _context.Medicines.Add(data);
            _context.SaveChanges();
            return _mapper.Map<MedicineDto>(data);
        }

        public MedicineDto DeleteMedicine(MedicineDto medicine)
        {
            var data = _mapper.Map<Medicine>(medicine);
            data.MedicineId = medicine.Id;
            _context.Medicines.Remove(data);
            _context.SaveChanges();
            return _mapper.Map<MedicineDto>(data);
        }

        public MedicineDto GetMedicineById(int id)
        {
            var data = _context.Medicines.Find(id);
            return _mapper.Map<MedicineDto>(data);
        }

        public List<MedicineDto> ListAll()
        {
            var data = from medicine in _context.Medicines
                       orderby medicine.Name
                       select new MedicineDto
                       {
                           Id = medicine.MedicineId,
                           Name = medicine.Name,
                           Description = medicine.Description,
                           Stock = medicine.Stock,
                           Generics = (from genericMedicine in _context.GenericMedications
                                       where genericMedicine.MedicineId == medicine.MedicineId
                                       join generic in _context.Generics on genericMedicine.GenericId equals generic.GenericId
                                       select new GenericDto
                                       {
                                           Id = generic.GenericId,
                                           Name = generic.Name,
                                           Description = generic.Description
                                       }).ToList()
                       };
            return data.ToList();
        }

        public MedicineDto UpdateMedicine(MedicineDto medicine)
        {
            var data = _mapper.Map<Medicine>(medicine);
            data.MedicineId = medicine.Id;
            _context.Medicines.Update(data);
            _context.SaveChanges();
            return medicine;
        }
    }
}
