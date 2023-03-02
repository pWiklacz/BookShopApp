using Microsoft.EntityFrameworkCore;
using BookShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopApp.Data;

namespace BookShopApp.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly BookShopAppDbContext _dbContext;

    public SqlRepository(BookShopAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        //_dbContext.Database.EnsureCreated();
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
