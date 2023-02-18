using BookShopApp.Entities;

namespace BookShopApp.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
}
