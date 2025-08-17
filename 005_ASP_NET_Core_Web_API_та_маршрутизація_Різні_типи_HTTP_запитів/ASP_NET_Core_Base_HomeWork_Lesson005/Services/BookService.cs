using ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces;

namespace ASP_NET_Core_Base_HomeWork_Lesson005.Services
{
    public class BookService : IBookService
    {
        private static readonly List<Book> _books = new();

        private readonly IBbookValidationService _bookValidationService;
        private readonly IBookFilterService _bokFilterService;

        public BookService(IBbookValidationService bookValidationService, IBookFilterService bokFilterService)
        {
            _bookValidationService = bookValidationService;
            _bokFilterService = bokFilterService;
        }

        public bool Add(Book book)
        {
            if (!_bookValidationService.AddBookValidation(book))
            {
                return false;
            }
            _books.Add(book);
            return true;
        }

        public bool Delete(Guid id)
        {
            var deletedBook = _books.FirstOrDefault(b => b.Id == id);
            if (deletedBook != null)
            {
                _books.Remove(deletedBook);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public IEnumerable<Book> GetAll(BookFilter filter)
        {
            return _bokFilterService.Filter(_books, filter);
        }

        public Book? GetById(Guid id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public bool Update(Book book)
        {
            var updatedBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (updatedBook != null)
            {
                updatedBook.Title = book.Title;
                updatedBook.Author = book.Author;
                updatedBook.PublishedYear = book.PublishedYear;
                return true;
            }
            return false;
        }
    }
}
