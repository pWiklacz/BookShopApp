using BookShopApp.Data.Entities;

namespace BookShopApp.Components.CsvReader;

public interface ICsvReader
{
    List<Book> ProcessBooks(string filepath);

    List<Author> ProcessAuthor(string filepath);
}