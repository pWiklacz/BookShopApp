using BookShopApp.Data;
using BookShopApp.Entities;
using BookShopApp.Repositories;

namespace BookShopApp;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly IRepository<Book> _repository;
    private readonly BookShopAppDbContext _bookShopAppDbContext;

    public App(IUserCommunication userCommunication, IRepository<Book> repository, BookShopAppDbContext bookShopAppDbContext)
    {
        this._userCommunication = userCommunication;
        this._repository = repository;
        this._bookShopAppDbContext = bookShopAppDbContext;
        this._bookShopAppDbContext.Database.EnsureCreated();
    }
    public void Run()
    {
        _userCommunication.MainMenu();
    }
}
