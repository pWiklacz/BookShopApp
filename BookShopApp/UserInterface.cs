using BookShopApp.Data;
using BookShopApp.Entities;
using BookShopApp.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace BookShopApp;

public class UserInterface
{
    public static void Run()
    {
        bool isRun = true;
        var repository = new SqlRepository<Book>(new BookShopAppDbContext());
        repository.ItemRemoved += OnItemRemoved;
        repository.ItemAdded += OnItemAdded;

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
                    AddItem(repository, book);
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    WriteAllToConsole(repository);
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    int id = GetBookId();
                    Book? bookToDelete = repository.GetById(id);
                    if (bookToDelete != null)
                        DeleteItem(repository, bookToDelete);
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
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Console.Clear();
            }
        }
    }

    private static void OnItemAdded(object? sender, Book e)
    {
        using (StreamWriter sw = new("audit_file.txt", true))
        {
            sw.WriteLine(DateTime.Now.ToString() + $" - Book added:\n" +
                $"{e}\n" +
                $"###########################################################\n");
        }
    }

    private static void OnItemRemoved(object? sender, Book e)
    {
        using (StreamWriter sw = new("audit_file.txt", true))
        {
            sw.WriteLine(DateTime.Now.ToString() + $" - Book removed:\n" +
                $"{e}\n" +
                $"###########################################################\n");
        }
    }

    static void AddItem<T>(IWriteRepository<T> repository, T item) where T : class, IEntity
    {
        repository.Add(item);
        repository.Save();
    }

    static int GetBookId()
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

    static Book CreateBook()
    {
        while (true)
        {
            Console.WriteLine("Enter title : ");
            var title = Console.ReadLine();
            Console.WriteLine("Enter author : ");
            var author = Console.ReadLine();
            Console.WriteLine("Enter book's edition (HardCover,SoftCover,AudioBook,EBook) : ");
            var edition = Console.ReadLine();

            if (!String.IsNullOrEmpty(title)
                && !String.IsNullOrEmpty(author)
                && !String.IsNullOrEmpty(edition))
            {
                switch (edition)
                {
                    case "HardCover":
                        return new Book
                        {
                            Title = title,
                            Author = author,
                            BookType = BookType.HardCover
                        };
                    case "SoftCover":
                        return new Book
                        {
                            Title = title,
                            Author = author,
                            BookType = BookType.SoftCover
                        };
                    case "AudioBook":
                        return new Book
                        {
                            Title = title,
                            Author = author,
                            BookType = BookType.AudioBook
                        };
                    case "EBook":
                        return new Book
                        {
                            Title = title,
                            Author = author,
                            BookType = BookType.EBook
                        };
                    default:
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fields must not be empty!");
            }
        }
    }
    static void DeleteItem<T>(IWriteRepository<T> repository, T item) where T : class, IEntity
    {
        repository.Remove(item);
        repository.Save();
    }
    static void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
        var items = repository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}
