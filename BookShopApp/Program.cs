using BookShopApp;
using BookShopApp.Data;
using BookShopApp.DataProviders;
using BookShopApp.Entities;
using BookShopApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IRepository<Book>, SqlRepository<Book>>();
services.AddSingleton<IBookProvider, BookProvider>();
services.AddDbContext<BookShopAppDbContext>(options => options
.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database=BookShopDB;Trusted_connection=True;"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

