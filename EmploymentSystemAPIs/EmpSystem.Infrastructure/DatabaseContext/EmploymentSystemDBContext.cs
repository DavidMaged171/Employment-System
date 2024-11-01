using EmpSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpSystem.Infrastructure.DatabaseContext
{
    public class EmploymentSystemDBContext : DbContext
    {
        public partial class EmploymentSystemContext : DbContext
        {
            public EmploymentSystemContext()
            {
            }

            public EmploymentSystemContext(DbContextOptions<EmploymentSystemContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                    optionsBuilder.UseSqlServer("Data Source=DESKTOP-TVTRTHB;Initial Catalog=EmploymentSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
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
}
