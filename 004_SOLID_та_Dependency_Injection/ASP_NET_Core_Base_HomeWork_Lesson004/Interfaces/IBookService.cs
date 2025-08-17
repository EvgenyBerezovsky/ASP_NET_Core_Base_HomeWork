namespace ASP_NET_Core_Base_HomeWork_Lesson004.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(Guid id);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Guid id);
    }
}
