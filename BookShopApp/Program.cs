using BookShopApp.Application;
using BookShopApp.Components.CsvReader;
using BookShopApp.Components.DataProviders;
using BookShopApp.Data;
using BookShopApp.Data.Entities;
using BookShopApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IRepository<Book>, SqlRepository<Book>>();
services.AddSingleton<IRepository<Author>, SqlRepository<Author>>();
services.AddSingleton<IBookProvider, BookProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddDbContext<BookShopAppDbContext>(options => options
.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB;Database=BookShopDB;Trusted_connection=True;"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

