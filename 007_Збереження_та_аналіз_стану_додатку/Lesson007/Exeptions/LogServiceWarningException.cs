namespace Lesson007.Exeptions
{
    public class LogServiceWarningException : Exception
    {
        public LogServiceWarningException(string message) : base(message){ }
    }
}
