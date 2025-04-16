using AutoMapper;
using MedicalStock.Data;
using MedicalStock.Model;
using MedicalStock.Model.Dtos;
using MedicalStock.Repository.IRepository;

namespace MedicalStock.Repository
{
    public class GenericRepository:IGenericRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(DataContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public GenericDto CreateGeneric(GenericDto generic)
        {
           var data =_mapper.Map<Generic>(generic);
            _context.Generics.Add(data);    
            _context.SaveChanges();
            return generic;
        }

        public GenericDto DeleteGeneric(GenericDto generic)
        {
            var data = _mapper.Map<Generic>(generic);
            data.GenericId = generic.Id;
            _context.Generics.Remove(data);
            _context.SaveChanges();
            return generic;
        }

        public GenericDto GetGenericById(int id)
        {
            var data = _context.Generics.Find(id);
            return _mapper.Map<GenericDto>(data);

        }

        public List<GenericDto> ListAll()
        {
            var data = from generic in _context.Generics
                       orderby generic.Name
                       select new GenericDto
                       {
                           Id = generic.GenericId,
                           Name = generic.Name,
                           Description= generic.Description
                       };
            return data.ToList();
        }

        public GenericDto UpdateGeneric(GenericDto generic)
        {
            var data = _mapper.Map<Generic>(generic);
            data.GenericId = generic.Id;
            _context.Generics.Update(data);
            _context.SaveChanges();
            return generic;
        }
    }
}
