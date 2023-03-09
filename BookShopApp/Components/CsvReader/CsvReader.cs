using BookShopApp.Components.CsvReader.Extensions;
using BookShopApp.Data.Entities;

namespace BookShopApp.Components.CsvReader;

public class CsvReader : ICsvReader
{
    public List<Book> ProcessBooks(string filepath)
    {
        if (!File.Exists(filepath))
        {
            return new List<Book>();
        }

        var books = File.ReadAllLines(filepath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToBooks();
        return books.ToList();
    }

    public List<Author> ProcessAuthor(string filepath)
    {
        if (!File.Exists(filepath))
        {
            return new List<Author>();
        }

        return File.ReadAllLines(filepath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .Select(x =>
            {
                var columns = x.Split(',');
                return new Author
                {
                    FullName = columns[0],
                    Email = columns[1],
                    Website = columns[2],
                    Country = columns[3]
                };
            }).ToList();
    }
}