using MedicalStock.Model;
using Microsoft.EntityFrameworkCore;

namespace MedicalStock.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Generic> Generics { get; set; }
        public DbSet<GenericMedicine> GenericMedications { get; set; }
        public DbSet<Medicine> Medicines{ get; set; }
    }
}
