using BookShopApp.Data;

namespace BookShopApp.Application;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly BookShopAppDbContext _bookShopAppDbContext;

    public App(IUserCommunication userCommunication, BookShopAppDbContext bookShopAppDbContext)
    {
        _userCommunication = userCommunication;
        _bookShopAppDbContext = bookShopAppDbContext;
        _bookShopAppDbContext.Database.EnsureCreated();
    }
    public void Run()
    {
        _userCommunication.MainMenu();
    }
}
