using Microsoft.EntityFrameworkCore;
namespace biblioteka13263.Models
{
public class BooksDbContext : DbContext
{
public BooksDbContext(DbContextOptions<BooksDbContext>options)
: base(options)
{
}
public DbSet<Book> Books { get; set; }
}
}
