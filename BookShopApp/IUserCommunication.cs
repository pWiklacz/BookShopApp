using BookShopApp.Entities;
using BookShopApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopApp
{
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
    }
}
