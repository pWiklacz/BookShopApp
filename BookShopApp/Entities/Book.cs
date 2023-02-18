namespace BookShopApp.Entities;

public class Book : EntityBase
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public BookType BookType { get; set; }
    public override string ToString() => $"Id: {Id}\n" +
        $"Title: {Title}\n" +
        $"Author {Author}\n" +
        $"Edition: {BookType}\n";
}
public enum BookType
{
    HardCover,
    SoftCover,
    AudioBook,
    EBook
}
