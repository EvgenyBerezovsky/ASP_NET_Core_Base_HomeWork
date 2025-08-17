namespace ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetAll(BookFilter filter);
        Book? GetById(Guid id);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Guid id);
    }
}
