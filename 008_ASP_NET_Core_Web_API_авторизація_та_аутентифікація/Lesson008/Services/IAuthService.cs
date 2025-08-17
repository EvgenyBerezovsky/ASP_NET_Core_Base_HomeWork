using Lesson008.Models;   

namespace Lesson008.Services
{
    public interface IAuthService
    {
        public string? LoginUser(LoginRequest loginRequest);
    }
}
