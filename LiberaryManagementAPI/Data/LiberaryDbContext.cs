using LiberaryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LiberaryManagementAPI.Data
{
    public class LiberaryDbContext : DbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<Loan> loans { get; set; }
        public DbSet<Category> categories { get; set; }
        public LiberaryDbContext(DbContextOptions<LiberaryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>().HasOne(b => b.Category).WithMany(b => b.Books)
                .HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Loan>()
                .HasOne(b => b.Book).WithMany(l => l.Loans).HasForeignKey(b => b.BookId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Loan>()
                .HasOne(m => m.Member).WithMany(l => l.loans).HasForeignKey(f => f.MemberId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
            modelBuilder.Entity<Member>().HasIndex( x=>x.Email).IsUnique();
        }
    }
}
