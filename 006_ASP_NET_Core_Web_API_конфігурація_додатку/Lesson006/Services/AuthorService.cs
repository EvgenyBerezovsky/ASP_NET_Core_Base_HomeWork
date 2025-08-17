using ASP_NET_Core_Base_HomeWork_Lesson006.Interfaces;
using Lesson006;
using Microsoft.Extensions.Options;

namespace ASP_NET_Core_Base_HomeWork_Lesson006.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IOptions<CastomConfigurationData> _configurationOPptions;
        private readonly List<string> _authors;

        public AuthorService(IOptions<CastomConfigurationData> configurationOptions)
        {
            _configurationOPptions = configurationOptions;
            _authors = _configurationOPptions.Value.DefaultAuthors ?? new List<string>();
        }
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
