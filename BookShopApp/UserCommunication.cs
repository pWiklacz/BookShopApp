using BookShopApp.Data;
using BookShopApp.DataProviders;
using BookShopApp.Entities;
using BookShopApp.Repositories;


namespace BookShopApp;

public class UserCommunication : IUserCommunication
{
    private readonly IRepository<Book> _repository;
    private readonly IBookProvider _bookProvider;

    public UserCommunication(IRepository<Book> repository, IBookProvider bookProvider)
    {
        _repository = repository;
        _bookProvider = bookProvider;
    }
    public void MainMenu()
    {
        var isRun = true;
        _repository.ItemRemoved += OnItemRemoved;
        _repository.ItemAdded += OnItemAdded;
        while (isRun)
        {
            Console.WriteLine(
                $"Welcome In BookShop Application!\n\n" +
                "1. Add book\n" +
                "2. Display available books\n" +
                "3. Delete book\n\n" +
                "To end program press ESC!");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Book book = CreateBook();
                    AddItem(_repository, book);
                    break;
                case ConsoleKey.D2:
                    displayBookMenu(_repository);
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    int id = GetBookId();
                    Book? bookToDelete = _repository.GetById(id);
                    if (bookToDelete != null)
                        DeleteItem(_repository, bookToDelete);
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Book with the given id does not exist!");
                    }
                    break;
                case ConsoleKey.Escape:
                    isRun = false;
                    break;
                default:
                    Console.Clear();
                    Console.Write("Wrong key selected!\n");
                    break;
            }

            if (isRun)
            {
                Console.Write("Press <Enter> to continue... ");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                }

                Console.Clear();
            }
        }
    }

    public void OnItemAdded(object? sender, Book e)
    {
        using StreamWriter sw = new("audit_file.txt", true);
        sw.WriteLine(DateTime.Now.ToString() + $" - Book added:\n" +
                     $"{e}\n" +
                     $"###########################################################\n");
    }

    public void OnItemRemoved(object? sender, Book e)
    {
        using StreamWriter sw = new("audit_file.txt", true);
        sw.WriteLine(DateTime.Now.ToString() + $" - Book removed:\n" +
                     $"{e}\n" +
                     $"###########################################################\n");
    }

    public void AddItem<T>(IWriteRepository<T> repository, T item) where T : class, IEntity
    {
        repository.Add(item);
        repository.Save();
    }

    public int GetBookId()
    {
        while (true)
        {
            Console.WriteLine("To delete a book enter its id: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int id))
                return id;
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect data was given! Positive integer numbers are required!");
            }
        }
    }

    public void displayBookMenu(IReadRepository<IEntity> repository)
    {
        var isRun = true;
        while (isRun)
        {
            Console.Clear();
            Console.WriteLine("1. Filter from lowest price");
            Console.WriteLine("2. Filter from highest price");
            Console.WriteLine("3. Find cheaper than");
            Console.WriteLine("Press ESC to back to menu.");
            WriteAllToConsole(repository);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    foreach (var item in _bookProvider.OrderByPrice())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    foreach (var item in _bookProvider.OrderByPriceDescending())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.WriteLine("Enter price: ");
                    var price = Console.ReadLine();
                    if (Decimal.TryParse(price, out var d))
                    {
                        foreach (var item in _bookProvider.WherePriceIsEqualOrLowerThan(d))
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong data was given!\n" +
                                          "Please try again.");
                    }
                    break;
                case ConsoleKey.Escape:
                    isRun = false;
                    break;
                default:
                    Console.Clear();
                    Console.Write("Wrong key selected!\n");
                    break;
            }
            if (isRun)
            {
                Console.Write("Press <Enter> to continue... ");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                }

                Console.Clear();
            }
        }
    }
    public Book CreateBook()
    {
        while (true)
        {
            Console.WriteLine("Enter title : ");
            var title = Console.ReadLine();
            Console.WriteLine("Enter author : ");
            var author = Console.ReadLine();
            Console.WriteLine("Enter book's edition (HardCover,SoftCover,AudioBook,EBook) : ");
            var edition = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            var price = Console.ReadLine();
            Console.WriteLine("Enter number of pages:");
            var pages = Console.ReadLine();

            

            if (!string.IsNullOrEmpty(title)
                && !string.IsNullOrEmpty(author)
                && !string.IsNullOrEmpty(edition)
                && !string.IsNullOrEmpty(price)
                && !string.IsNullOrEmpty(pages))
            {
                if (Decimal.TryParse(price, out var priceResult)
                    && int.TryParse(pages, out var pagesResult))
                {
                    switch (edition)
                    {
                        case "HardCover":
                            return new Book
                            {
                                Title = title,
                                Author = author,
                                BookType = BookType.HardCover,
                                Price = priceResult,
                                NumberOfPages = pagesResult
                            };
                        case "SoftCover":
                            return new Book
                            {
                                Title = title,
                                Author = author,
                                BookType = BookType.SoftCover,
                                Price = priceResult,
                                NumberOfPages = pagesResult
                            };
                        case "AudioBook":
                            return new Book
                            {
                                Title = title,
                                Author = author,
                                BookType = BookType.AudioBook,
                                Price = priceResult,
                                NumberOfPages = pagesResult
                            };
                        case "EBook":
                            return new Book
                            {
                                Title = title,
                                Author = author,
                                BookType = BookType.EBook,
                                Price = priceResult,
                                NumberOfPages = pagesResult
                            };
                        default:
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong data was given!\n" +
                                      "Please try again.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fields must not be empty!");
            }
        }
    }
    public void DeleteItem<T>(IWriteRepository<T> repository, T item) where T : class, IEntity
    {
        repository.Remove(item);
        repository.Save();
    }
    public void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
        var items = repository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}
