using BookShopApp.Entities;

namespace BookShopApp.Repositories;

internal class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly List<T> _items = new();

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Save()
    {
        //Nothing to do
    }

    public T GetById(int id) 
    {
        return _items.Single(item => item.Id == id);
    }

    public List<T> Get_items()
    {
        return _items;
    }

    public void Remove(T item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public IEnumerable<T> GetAll() => _items.ToList();

}
