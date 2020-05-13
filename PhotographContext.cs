using System.Data.Entity;

namespace EFStudiiDeCaz
{
    class PhotographContext : DbContext
    {
        public DbSet<Model.Photograph> Photographs { get; set; }
        
        public DbSet<Model.PhotographFullImage> PhotographFullImages { get; set; }
        
        public PhotographContext() : base("name=PhotographContext") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder
                .Entity<Model.Photograph>()
                .HasRequired(p => p.PhotographFullImage)
                .WithRequiredPrincipal(p => p.Photograph);
            modelBuilder
                .Entity<Model.Photograph>()
                .ToTable("Photograph", "BazaDeDate");
            modelBuilder.Entity<Model.PhotographFullImage>()
                .ToTable("Photograph", "BazaDeDate");
        }
    }
}
