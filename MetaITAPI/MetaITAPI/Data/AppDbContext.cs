using MetaITAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetaITAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                AuthorId = 1,
                FirstName = "FirstName1",
                LastName = "LastName1"
            },
            new Author
            {
                AuthorId = 2,
                FirstName = "FirstName2",
                LastName = "LastName2"
            }
        );
    }
}
