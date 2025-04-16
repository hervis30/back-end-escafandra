using AutoMapper;
using MedicalStock.Model;
using MedicalStock.Model.Dtos;

namespace MedicalStock.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Generic, GenericDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GenericId))
                .ReverseMap();

            CreateMap<GenericMedicine, GenericMedicineDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GenericMedicationId))
                .ReverseMap();

            CreateMap<Medicine, MedicineDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MedicineId))
                .ReverseMap();
        }
    }
}
