namespace HabitLogger;

internal class HabitLoggerRepository
{
    private List<Log> logs = new List<Log>();

    internal void Delete(DateOnly date)
    {
        Log? searchLog = FindLogBasedOnEntryDate(date);
        if (searchLog == null)
        {
            throw new InvalidDataException();
        }
        logs.Remove(searchLog);
    }

    internal List<Log> FindAllLogs() => logs;

    internal void Insert(Log log)
    {
        Log? searchLog = FindLogBasedOnEntryDate(log.DateOfEntry);
        if (searchLog != null)
        {
            throw new InvalidOperationException();
        }
        logs.Add(log);
    }

    internal void Update(Log log)
    {
        Log? searchLog = FindLogBasedOnEntryDate(log.DateOfEntry);
        if (searchLog == null)
        {
            throw new InvalidDataException();
        }
        logs.Remove(searchLog);
        logs.Add(log);
    }

    private Log? FindLogBasedOnEntryDate(DateOnly date) => logs.Find(e => e.DateOfEntry.Equals(date));
}