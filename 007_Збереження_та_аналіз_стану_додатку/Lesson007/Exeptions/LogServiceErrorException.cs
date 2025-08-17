namespace Lesson007.Exeptions
{
    public class LogServiceErrorException : Exception
    {
        public LogServiceErrorException(string message) : base(message)
        {
                
        }
    }
}
