using Microsoft.EntityFrameworkCore;
using UsingInMemoryCache.Models;

namespace UsingInMemoryCache.Data
{
    public partial class CourseLibraryDbContext : DbContext
    {
        public CourseLibraryDbContext()
        {
        }

        public CourseLibraryDbContext(DbContextOptions<CourseLibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SHL-34;Database=CourseLibraryDB;user id=sa;password=P@ssw0rdM@n;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Persian_100_CI_AI");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MainCategory)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.AuthorId, "IX_Courses_AuthorId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(1500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.AuthorId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
