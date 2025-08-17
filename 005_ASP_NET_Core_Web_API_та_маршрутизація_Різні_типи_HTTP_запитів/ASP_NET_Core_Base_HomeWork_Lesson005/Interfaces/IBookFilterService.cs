namespace ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces
{
    public interface IBookFilterService
    {
        public IEnumerable<Book> Filter(IEnumerable<Book> input, BookFilter filter);
    }
}
