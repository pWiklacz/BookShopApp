using BookShopApp.Entities;

namespace BookShopApp.Repositories;

internal class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
    }

    public void Save()
    {
        //Nothing to do
    }

    public T GetById(int id) 
    {
        return _items.Single(item => item.Id == id);
    }

    public void Remove(T item) => _items.Remove(item);

    public IEnumerable<T> GetAll() => _items.ToList();

}
