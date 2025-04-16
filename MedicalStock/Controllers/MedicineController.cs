using AutoMapper;
using MedicalStock.Model;
using MedicalStock.Model.Dtos;
using MedicalStock.Repository;
using MedicalStock.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MedicalStock.Controllers
{
    [ApiController]
    [Route("api/medicine")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IExcelExportRepository _excelRepository;
        private readonly IGenericMedicineRepository _genericMedicineRepository;

        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public MedicineController(IMedicineRepository medicineRepository, IGenericMedicineRepository genericMedicineRepository, IExcelExportRepository excelExportRepository, IMapper mapper, IConfiguration configuration)
        {
            _medicineRepository = medicineRepository;
            _excelRepository = excelExportRepository;
            _genericMedicineRepository = genericMedicineRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] MedicineDto medicineDto)
        {
            //crear medicamentos
            var medicine = _medicineRepository.CreateMedicine(medicineDto);

            //crear génericos
            if (medicineDto.Generics != null && medicineDto.Generics.Any())
            {
                foreach (var item in medicineDto.Generics)
                {
                    var relationDto = new GenericMedicineDto
                    {
                        Medicines = medicine,
                        Generics = item
                    };

                    _genericMedicineRepository.CreateGenericMedicine(relationDto);
                }
            }
            return Ok(medicine);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] MedicineDto medicineDto)
        {
            //actualizar medicamentos
            var medicine = _medicineRepository.UpdateMedicine(medicineDto);

            //elimina génericos
            _genericMedicineRepository.DeleteByMedicineId(medicine.Id);

            //crea génericos
            if (medicineDto.Generics != null && medicineDto.Generics.Any())
            {
                foreach (var item in medicineDto.Generics)
                {
                    var relationDto = new GenericMedicineDto
                    {
                        Medicines = medicine,
                        Generics = item
                    };

                    _genericMedicineRepository.CreateGenericMedicine(relationDto);
                }
            }

            return Ok(medicine);
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] MedicineDto medicineDto)
        {
            //eliminar relaciones con génericos
            _genericMedicineRepository.DeleteByMedicineId(medicineDto.Id);

            //eliminar medicamento
            var medicine = _medicineRepository.DeleteMedicine(medicineDto);
            return Ok(medicine);
        }

        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            var generic = _medicineRepository.ListAll();
            if (generic != null)
                return Ok(generic);
            return NoContent();
        }

        [HttpGet("ReadMedicine")]
        public IActionResult ReadMedicine([FromQuery] int id)
        {
            var generic = _medicineRepository.GetMedicineById(id);
            generic.Id = id;
            if (generic != null)
                return Ok(generic);
            return NoContent();
        }

        [HttpGet("ExportToExcel")]
        public IActionResult ExportToExcel()
        {

            var excelBytes = _excelRepository.ExportToExcel();

            return File(excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Medicamentos.xlsx");
        }

        [HttpGet("ExportToPdf")]
        public IActionResult ExportToPdf()
        {
            var pdfBytes = _excelRepository.ExportToPdf();

            return File(pdfBytes,
                "application/pdf", "Medicamentos.pdf");
        }

    }
}
