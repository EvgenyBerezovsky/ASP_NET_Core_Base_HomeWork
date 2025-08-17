using ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces;

namespace ASP_NET_Core_Base_HomeWork_Lesson005.Services
{
    public class BookFilterService : IBookFilterService
    {
        public IEnumerable<Book>? Filter(IEnumerable<Book> input, BookFilter filter)
        {
            if (filter.Author == null)
            {
                return input;
            }  
            
            var filteredBooks = input.Where(book => book.Author.Equals(filter.Author) && 
                                                    book.PublishedYear == filter.PublishedYear); 
            return filteredBooks;
        }

    }
}
