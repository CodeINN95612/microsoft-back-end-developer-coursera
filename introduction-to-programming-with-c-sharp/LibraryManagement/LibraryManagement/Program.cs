
var library = new LibraryManagementSystem(5);
library.Run();

class Book
{
    public string Title { get; set; }
    public bool IsAvailable { get; set; }

    public string Status => IsAvailable ? "Available" : "Borrowed";

    public Book(string title, bool isAvailable)
    {
        Title = title;
        IsAvailable = isAvailable;
    }
}

class LibraryManagementSystem
{
    private readonly List<Book> _books;
    private readonly int _maxBooks;

    private const int MaxBorrowedBooks = 3;
    private int _borrowedBooksCount;

    public LibraryManagementSystem(int maxBooks)
    {
        _books = [];
        _maxBooks = maxBooks;
        _borrowedBooksCount = 0;
    }

    private void AddBook(string title)
    {
        var book = new Book(title, true);
        _books.Add(book);
    }

    private void RemoveBook(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        if (book is not null)
        {
            _books.Remove(book);
        }
    }

    private void SearchBookByTitle(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        if (book is not null)
        {
            Console.WriteLine("Book found: " + book.Title + " (" + book.Status + ")");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    private void BorrowBook(string title)
    {
        if (_borrowedBooksCount >= MaxBorrowedBooks)
        {
            Console.WriteLine("You have reached the maximum number of borrowed books.");
            return;
        }
        var book = _books.FirstOrDefault(b => b.Title == title);
        if (book is not null && book.IsAvailable)
        {
            book.IsAvailable = false;
            _borrowedBooksCount++;
            Console.WriteLine("You have borrowed: " + book.Title);
        }
        else
        {
            Console.WriteLine("Book not available for borrowing.");
        }
    }

    private void ReturnBook(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        if (book is not null && !book.IsAvailable)
        {
            book.IsAvailable = true;
            _borrowedBooksCount--;
            Console.WriteLine("You have returned: " + book.Title);
        }
        else
        {
            Console.WriteLine("Book not found or already available.");
        }
    }

    private void DisplayBooks()
    {
        Console.WriteLine("Books in the library:");

        if (_books.Count == 0)
        {
            Console.WriteLine("No books found");
        }

        foreach (var book in _books)
        {
            Console.WriteLine("- " + book.Title + " (" + book.Status + ")");
        }
        Console.WriteLine();
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Display Books");
            Console.WriteLine("4. Search Book by Title");
            Console.WriteLine("5. Borrow Book");
            Console.WriteLine("6. Return Book");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter book title: ");
                    var titleToAdd = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(titleToAdd))
                    {
                        Console.WriteLine("Title cannot be empty.");
                        break;
                    }
                    if (_books.Any(b => b.Title == titleToAdd))
                    {
                        Console.WriteLine("Book already exists.");
                        break;
                    }
                    if (_books.Count >= _maxBooks)
                    {
                        Console.WriteLine("Library is full, cannot add more books.");
                        break;
                    }

                    AddBook(titleToAdd);
                    break;
                case "2":
                    Console.Write("Enter book title to remove: ");
                    var titleToRemove = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(titleToRemove))
                    {
                        Console.WriteLine("Title cannot be empty.");
                        break;
                    }
                    if (!_books.Any(b => b.Title == titleToRemove))
                    {
                        Console.WriteLine("Book not found.");
                        break;
                    }
                    if (!_books.First(b => b.Title == titleToRemove).IsAvailable)
                    {
                        Console.WriteLine("Book is currently borrowed and cannot be removed.");
                        break;
                    }

                    RemoveBook(titleToRemove);
                    break;
                case "3":
                    DisplayBooks();
                    break;
                case "4":
                    Console.Write("Enter book title to search: ");
                    var titleToSearch = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(titleToSearch))
                    {
                        Console.WriteLine("Title cannot be empty.");
                        break;
                    }
                    SearchBookByTitle(titleToSearch);
                    break;
                case "5":
                    Console.Write("Enter book title to borrow: ");
                    var titleToBorrow = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(titleToBorrow))
                    {
                        Console.WriteLine("Title cannot be empty.");
                        break;
                    }
                    if (!_books.Any(b => b.Title == titleToBorrow))
                    {
                        Console.WriteLine("Book not found.");
                        break;
                    }
                    if (!_books.First(b => b.Title == titleToBorrow).IsAvailable)
                    {
                        Console.WriteLine("Book is already borrowed.");
                        break;
                    }
                    BorrowBook(titleToBorrow);
                    break;
                case "6":
                    Console.Write("Enter book title to return: ");
                    var titleToReturn = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(titleToReturn))
                    {
                        Console.WriteLine("Title cannot be empty.");
                        break;
                    }
                    if (!_books.Any(b => b.Title == titleToReturn))
                    {
                        Console.WriteLine("Book not found.");
                        break;
                    }
                    if (_books.First(b => b.Title == titleToReturn).IsAvailable)
                    {
                        Console.WriteLine("Book is already available.");
                        break;
                    }
                    ReturnBook(titleToReturn);
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Exiting the library management system.");
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}
