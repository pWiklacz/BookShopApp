using BookShopApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShopApp.Data.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly BookShopAppDbContext _dbContext;

    public SqlRepository(BookShopAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public void Save() => _dbContext.SaveChanges();

    public IEnumerable<T> GetAll() => _dbSet.ToList();

    public void Add(T item)
    {

        _dbSet.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public T? GetById(int id) => _dbSet.Find(id);
}
