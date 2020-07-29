using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace Student.API
{
  public partial class SchoolManagementContext : DbContext
  {
    public SchoolManagementContext()
    {
    }

    public SchoolManagementContext(DbContextOptions<SchoolManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Course { get; set; }
    public virtual DbSet<Student> Student { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var appSettings = ConfigurationManager.AppSettings;
        if(appSettings.Count == 0)
        {
          throw new Exception("AppSettings is empty.");
        }

        var connectionString = appSettings.Get("SchoolManagementContext");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
          throw new ArgumentNullException(nameof(connectionString));
        }

        optionsBuilder.UseSqlServer(connectionString);
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Course>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.HasOne(d => d.Student)
                  .WithMany(p => p.Course)
                  .HasForeignKey(d => d.StudentId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Course_Student");
      });

      modelBuilder.Entity<Student>(entity =>
      {
        entity.HasKey(e => e.RepoId);

        entity.Property(e => e.RepoId).ValueGeneratedNever();

        entity.Property(e => e.Address)
                  .IsRequired()
                  .HasMaxLength(250);

        entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(100);

        entity.Property(e => e.Id)
                  .IsRequired()
                  .HasMaxLength(50);

        entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(100);

        entity.Property(e => e.PhoneNumber).HasMaxLength(50);

        entity.Property(e => e.Status)
                  .IsRequired()
                  .HasMaxLength(50);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
