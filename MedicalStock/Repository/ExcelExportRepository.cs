using AutoMapper;
using ClosedXML.Excel;
using MedicalStock.Data;
using MedicalStock.Model;
using MedicalStock.Model.Dtos;
using MedicalStock.Repository.IRepository;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace MedicalStock.Repository
{
    public class ExcelExportRepository : IExcelExportRepository
    {
        private readonly IMedicineRepository _medicineRepository;

        public ExcelExportRepository(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }

        public byte[] ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {

                List<MedicineDto> medicines = new List<MedicineDto>();
                medicines = _medicineRepository.ListAll();

                var worksheet = workbook.Worksheets.Add("Medicamentos");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nombre";
                worksheet.Cell(1, 3).Value = "Descripción";
                worksheet.Cell(1, 4).Value = "Stock";
                worksheet.Cell(1, 5).Value = "Genéricos";

                // Estilo para los encabezados
                var headerRange = worksheet.Range("A1:E1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                // Datos
                int row = 2;
                foreach (var medicine in medicines)
                {
                    worksheet.Cell(row, 1).Value = medicine.Id;
                    worksheet.Cell(row, 2).Value = medicine.Name;
                    worksheet.Cell(row, 3).Value = medicine.Description;
                    worksheet.Cell(row, 4).Value = medicine.Stock;
                    worksheet.Cell(row, 5).Value = string.Join(", ", medicine.Generics.Select(g => g.Name));

                    row++;
                }

                // Autoajustar columnas
                worksheet.Columns().AdjustToContents();

                // Convertir a bytes
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] ExportToPdf()
        {
            var medicines = _medicineRepository.ListAll();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Header()
                        .Text("Lista de Medicamentos")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // ID
                                columns.RelativeColumn(); // Nombre
                                columns.RelativeColumn(); // Descripción
                                columns.RelativeColumn(); // Stock
                                columns.RelativeColumn(); // Genéricos
                            });

                            // Encabezados
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("ID");
                                header.Cell().Element(CellStyle).Text("Nombre");
                                header.Cell().Element(CellStyle).Text("Descripción");
                                header.Cell().Element(CellStyle).Text("Stock");
                                header.Cell().Element(CellStyle).Text("Genéricos");

                                IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold()).Padding(5).Background(Colors.Grey.Lighten3);
                                }
                            });

                            // Datos
                            foreach (var med in medicines)
                            {
                                table.Cell().Element(CellStyle).Text(med.Id.ToString());
                                table.Cell().Element(CellStyle).Text(med.Name);
                                table.Cell().Element(CellStyle).Text(med.Description);
                                table.Cell().Element(CellStyle).Text(med.Stock.ToString());
                                table.Cell().Element(CellStyle).Text(string.Join(", ", med.Generics?.Select(g => g.Name) ?? Enumerable.Empty<string>()));

                                IContainer CellStyle(IContainer container)
                                {
                                    return container.Padding(5);
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generado por MedicalStock");
                        });
                });
            });

            // Devuelve el PDF como byte[]
            return document.GeneratePdf();
        }
    }
}
