namespace Lesson007.Models
{
    public class CallState
    {
        public int Count { get; set; }
        public DateTime FirstCallTime { get; set; }

        public CallState(int count, DateTime firstCallTime)
        {
            Count = count;
            FirstCallTime = firstCallTime;
        }
    }
}
