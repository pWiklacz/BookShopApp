using BookShopApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShopApp.Data;

public class BookShopAppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public BookShopAppDbContext(DbContextOptions<BookShopAppDbContext> options)
        : base(options)
    {

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database=BookShopDB;Trusted_connection=True;");
    //}
}
