using BookShopApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShopApp.Data;

public class BookShopAppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public DbSet<Author> Authors => Set<Author>();

    public BookShopAppDbContext(DbContextOptions<BookShopAppDbContext> options)
        : base(options)
    {

    }
}
