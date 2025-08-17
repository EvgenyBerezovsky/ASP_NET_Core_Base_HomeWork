namespace ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces
{
    public interface IAuthorService
    {
        bool IsAuthorExist(string author);
        bool Add(string author);
        bool Delete(string author);
    }
}
