using ASP_NET_Core_Base_HomeWork_Lesson004.Interfaces;

namespace ASP_NET_Core_Base_HomeWork_Lesson004.Services
{
    public class BookValidationService : IBookValidationService
    {
        private IAuthorService _authorService;

        public BookValidationService(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public bool AddBookValidation(Book book)
        {
            return book.Title.Length < 30
                && book.PublishedYear < DateTime.Now.Date.Year
                && _authorService.IsAuthorExist(book.Author);
        }
    }
}
