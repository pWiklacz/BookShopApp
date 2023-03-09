using BookShopApp.Data.Entities;

namespace BookShopApp.Components.CsvReader.Extensions;

public static class BookExtensions
{
    public static IEnumerable<Book> ToBooks(this IEnumerable<string> books)
    {
        foreach (var book in books)
        {
            var columns = book.Split(',');

            yield return new Book
            {
                Title = columns[0],
                Author = columns[1],
                BookType = (BookType)Enum.Parse(typeof(BookType), columns[2]),
                Price = decimal.Parse(columns[3]),
                NumberOfPages = int.Parse(columns[4])
            };
        }
    }
}