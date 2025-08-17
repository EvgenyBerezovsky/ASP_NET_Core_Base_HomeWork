using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IDataRepository
    {
        public List<User> GetAllUsers();
        public List<Quiz> GetAllQuizzes();
        public Quiz? GetQuizByTopic(string topic);
        public User? GetUserByName(string name);
        public Quiz? CreateQuiz(Quiz quiz);
        public Quiz? UpdateQuiz(Quiz quiz);
        public Quiz? DeleteQuiz(Quiz quiz);
    }
}
