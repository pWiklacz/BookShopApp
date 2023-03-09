using BookShopApp.Data.Entities;
using BookShopApp.Data.Repositories;

namespace BookShopApp.Application;

public interface IUserCommunication
{
    void MainMenu();
    void OnItemAdded(object? sender, Book e);
    void OnItemRemoved(object? sender, Book e);
    void AddItem<T>(IWriteRepository<T> repository, T item) where T : class, IEntity;
    int GetBookId();
    Book CreateBook();
    void DeleteItem<T>(IWriteRepository<T> repository, T item) where T : class, IEntity;
    void WriteAllToConsole(IReadRepository<IEntity> repository);
    void DataFromCsv();
    void SaveToXml();
}