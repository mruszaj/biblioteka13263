using Microsoft.EntityFrameworkCore;
namespace biblioteka13263.Models
{
public class LibraryDbContext : DbContext


    { 

        public DbSet<Book> Books { get; set; } 

        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Worker> Workers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });  

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenre)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenre)
                .HasForeignKey(bg => bg.GenreId);

            
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Client)
                .HasForeignKey(b => b.ClientId);
            modelBuilder.Entity<Worker>()
                .HasIndex(b => b.Login)
                .IsUnique();
            modelBuilder.Entity<Client>()
               .HasIndex(b => b.Login)
               .IsUnique();
        }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
: base(options)
        {
        }

    }

}
