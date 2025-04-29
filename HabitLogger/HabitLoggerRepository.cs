namespace HabitLogger;

internal class HabitLoggerRepository : IHabitLoggerRepository
{
    private List<Log> logs = new List<Log>();

    public void DeleteLog(DateOnly date)
    {
        Log? searchLog = FindLogBasedOnEntryDate(date);
        if (searchLog == null)
        {
            throw new InvalidDataException($"No data could found for the entered date - {date.ToString(HabitLoggerConstants.DateFormat)}, unable to delete data");
        }
        logs.Remove(searchLog);
    }

    public List<Log> FindAllLogs() => logs;

    public void InsertLog(Log log)
    {
        Log? searchLog = FindLogBasedOnEntryDate(log.DateOfEntry);
        if (searchLog != null)
        {
            throw new InvalidOperationException("Data already exists for date entered");
        }
        logs.Add(log);
    }

    public void UpdateLog(Log log)
    {
        Log? searchLog = FindLogBasedOnEntryDate(log.DateOfEntry);
        if (searchLog == null)
        {
            throw new InvalidDataException($"No data could found for the entered date - {log.DateOfEntry.ToString(HabitLoggerConstants.DateFormat)}, unable to update data");
        }
        logs.Remove(searchLog);
        logs.Add(log);
    }

    private Log? FindLogBasedOnEntryDate(DateOnly date) => logs.Find(e => e.DateOfEntry.Equals(date));
}