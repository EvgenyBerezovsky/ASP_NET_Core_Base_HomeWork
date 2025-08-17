using ASP_NET_Core_Base_HomeWork_Lesson006.Interfaces;
using Lesson006;
using Microsoft.Extensions.Options;

namespace ASP_NET_Core_Base_HomeWork_Lesson006.Services
{
    public class BookService : IBookService
    {
        private readonly IOptions<CastomConfigurationData> _options;
        private readonly List<Book> _books = new();

        public BookService(IOptions<CastomConfigurationData> options)
        {
            _options = options;
            _books = _options.Value.DefaultBooks ?? new List<Book>();
        }

        public bool Add(Book book)
        {
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
