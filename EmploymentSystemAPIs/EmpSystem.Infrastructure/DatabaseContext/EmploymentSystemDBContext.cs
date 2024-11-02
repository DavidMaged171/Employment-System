using EmpSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmpSystem.Infrastructure.DatabaseContext
{
    public partial class EmploymentSystemDBContext : DbContext
    {

        public IConfiguration configuration { get; set; }
        public EmploymentSystemDBContext() { }
        public EmploymentSystemDBContext(DbContextOptions<EmploymentSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<Vacancy> Vacancies { get; set; } = null!;
        public virtual DbSet<VacancyApplications> VacancyApplications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                this.configuration = configurationBuilder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("EmploymentSystemDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.ToTable("Vacancy");
                entity.Property(e => e.VacancyId).HasColumnName("Id");
                entity.Property(e => e.VacancyName).HasColumnName("Name");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.VacancyName).HasMaxLength(50);
            });

            modelBuilder.Entity<VacancyApplications>(entity =>
            {
                entity.HasKey(e => new { e.ApplicantId, e.VacancyId });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
