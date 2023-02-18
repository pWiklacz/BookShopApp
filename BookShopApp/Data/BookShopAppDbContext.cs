using BookShopApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShopApp.Data;

public class BookShopAppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database=BookShopDB;Trusted_connection=True;");
    }
}
