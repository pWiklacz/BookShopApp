namespace BookShopApp.Entities;

public class Book : EntityBase
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public BookType BookType { get; set; }
    public decimal Price { get; set; }
    public int NumberOfPages { get; set; }
    public override string ToString() => $"Id: {Id}\n" +
        $"Title: {Title}\n" +
        $"Author {Author}\n" +
        $"Edition: {BookType}\n" +
        $"Number of pages: {NumberOfPages}\n" +
        $"Price: {Price}\n";
}
public enum BookType
{
    HardCover,
    SoftCover,
    AudioBook,
    EBook
}
