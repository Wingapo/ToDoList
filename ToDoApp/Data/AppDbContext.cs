using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note_Category>().HasKey(nc => new
            {
                nc.NoteId,
                nc.CategoryId
            });

            modelBuilder.Entity<Note_Category>()
                .HasOne(n => n.Note)
                .WithMany(nc => nc.Note_Category)
                .HasForeignKey(nc => nc.NoteId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Note_Category> Note_Category { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
