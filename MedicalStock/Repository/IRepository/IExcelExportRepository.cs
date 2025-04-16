using MedicalStock.Model.Dtos;

namespace MedicalStock.Repository.IRepository
{
    public interface IExcelExportRepository
    {
        byte[] ExportToExcel();

        byte[] ExportToPdf();
    }
}
