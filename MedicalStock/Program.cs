using MedicalStock.Data;
using MedicalStock.Mapper;
using MedicalStock.Repository;
using MedicalStock.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSql"));
});

//Inyección de dependencias repositorio
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IExcelExportRepository, ExcelExportRepository>();
builder.Services.AddScoped<IGenericMedicineRepository, GenericMedicineRepository>();

//Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//Atributos en mayuscula
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("policy");
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
