using System.Data.Entity;

namespace EFStudiiDeCaz
{
    class EmployeeContext : DbContext
    {
        public DbSet<Model.Employee> Employees { get; set; }

        public EmployeeContext() : base("name=EmployeeContext") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Model.Employee>()
                .Map<Model.FullTimeEmployee>(m => m.Requires("EmployeeType").HasValue(1))
                .Map<Model.HourlyEmployee>(m => m.Requires("EmployeeType").HasValue(2));
        }
    }
}
