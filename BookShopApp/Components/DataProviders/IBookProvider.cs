using BookShopApp.Data.Entities;

namespace BookShopApp.Components.DataProviders;

public interface IBookProvider
{
    decimal GetMinimumPriceOfAllBooks();
    List<Book> OrderByPrice();
    List<Book> OrderByPriceDescending();
    List<Book> WherePriceIsEqualOrLowerThan(decimal price);
}