namespace BookShopApp.Data.Entities;

public class Author : EntityBase
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? Country { get; set;}
}