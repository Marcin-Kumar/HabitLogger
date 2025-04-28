namespace HabitLogger;

internal class HabitLoggerRepository
{
    private List<Log> logs = new List<Log>();

    internal void Delete(DateTime dateTime)
    {
        Log searchLog = FindLogBasedOnEntryDate(dateTime);

        logs.Remove(searchLog);
    }

    internal List<Log> FindAllLogs() => logs;

    internal void Insert(Log log)
    {
        logs.Add(log);
    }

    internal void Update(DateTime dateTime, Log log)
    {
        Log searchLog = FindLogBasedOnEntryDate(dateTime);

        logs.Remove(searchLog);
        logs.Add(log);
    }
    private Log FindLogBasedOnEntryDate(DateTime dateTime)
    {
        Log? searchLog = logs.Find(e => e.DateOfEntry.Equals(dateTime));
        if (searchLog == null)
        {
            throw new InvalidOperationException();
        }
        return searchLog;
    }
}