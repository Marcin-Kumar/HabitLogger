namespace HabitLogger;
internal class Program
{
    static void Main(string[] args)
    {
        HabitLoggerManager.GetHabitLoggerManager(new HabitLoggerRepository()).Run();   
    }
}
