namespace Lesson007.Abstractions
{
    public interface ILogService
    {
        public bool LogInformation();
        public bool LogDebug();
        public bool LogWarning();
        public bool LogIError();
    }
}
