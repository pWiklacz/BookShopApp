using BookShopApp.Data.Entities;
using BookShopApp.Data.Repositories;

namespace BookShopApp.Components.DataProviders;

public class BookProvider : IBookProvider
{
    private readonly IRepository<Book> _bookRepository;

    public BookProvider(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public decimal GetMinimumPriceOfAllBooks()
    {
        var books = _bookRepository.GetAll();
        return books.Select(book => book.Price).Min();
    }

    public List<Book> OrderByPrice()
    {
        var books = _bookRepository.GetAll();
        return books.OrderBy(book => book.Price).ToList();
    }

    public List<Book> OrderByPriceDescending()
    {
        var books = _bookRepository.GetAll();
        return books.OrderByDescending(book => book.Price).ToList();
    }

    public List<Book> WherePriceIsEqualOrLowerThan(decimal price)
    {
        var books = _bookRepository.GetAll();
        return books.Where(x => x.Price <= price).ToList();
    }
}