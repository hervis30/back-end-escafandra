using AutoMapper;
using MedicalStock.Model.Dtos;
using MedicalStock.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MedicalStock.Controllers
{
    [ApiController]
    [Route("api/generic")]
    public class GenericController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IGenericMedicineRepository _genericMedicineRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public GenericController(IGenericRepository genericRepository, IGenericMedicineRepository genericMedicineRepository, IMapper mapper, IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _genericMedicineRepository = genericMedicineRepository;
            _mapper = mapper;
            _configuration = configuration;
        }


        [HttpPost("Create")]
        public IActionResult Create([FromBody] GenericDto genericDto)
        {
            var generic = _genericRepository.CreateGeneric(genericDto);
            return Ok(generic);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] GenericDto genericDto)
        {
            var generic = _genericRepository.UpdateGeneric(genericDto);
            return Ok(generic);
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] GenericDto genericDto)
        {
            var generic = _genericRepository.DeleteGeneric(genericDto);
            return Ok(generic);
        }

        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            var generic = _genericRepository.ListAll();
            if (generic != null)
                return Ok(generic);
            return NoContent();
        }

        [HttpGet("ReadGeneric")]
        public IActionResult ReadGeneric([FromQuery] int id)
        {
            var generic = _genericRepository.GetGenericById(id);
            generic.Id = id;
            if (generic != null)
                return Ok(generic);
            return NoContent();
        }

        [HttpGet("IsGenericInUse")]
        public IActionResult IsGenericInUse([FromQuery] int id)
        {
            bool isInUse = _genericMedicineRepository.IsGenericInUse(id);
            return Ok(isInUse);
        }

    }
}
