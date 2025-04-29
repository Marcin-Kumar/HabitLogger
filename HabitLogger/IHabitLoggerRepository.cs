namespace HabitLogger;

internal interface IHabitLoggerRepository
{
    public abstract void DeleteLog(DateOnly date);
    public abstract void InsertLog(Log log);
    public abstract void UpdateLog(Log log);
    public abstract List<Log> FindAllLogs();
}
