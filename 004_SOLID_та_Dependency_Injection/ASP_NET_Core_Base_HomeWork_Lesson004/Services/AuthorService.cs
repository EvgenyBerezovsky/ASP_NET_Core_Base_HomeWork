using ASP_NET_Core_Base_HomeWork_Lesson004.Interfaces;

namespace ASP_NET_Core_Base_HomeWork_Lesson004.Services
{
    public class AuthorService : IAuthorService
    {
        private static readonly List<string> _authors = new();

        public bool Add(string author)
        {
            if (!IsAuthorExist(author))
            {
                _authors.Add(author);
                return true;
            }
            return false;    
        }

        public bool Delete(string author)
        {
            if (IsAuthorExist(author))
            {
                _authors.Remove(author);
                return true;
            }
            return false;   
        }

        public bool IsAuthorExist(string author)
        {
            return _authors.Contains(author);
        }
    }
}
