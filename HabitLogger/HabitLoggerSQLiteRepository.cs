using System.Data.SQLite;

namespace HabitLogger;

internal class HabitLoggerSQLiteRepository : IHabitLoggerRepository
{
    private readonly string _connectionString;

    internal HabitLoggerSQLiteRepository(string connectionString)
    {
        _connectionString = connectionString;
        createTableIfDoesntExist();
    }

    public void DeleteLog(DateOnly date)
    {
        try
        {
            using SQLiteConnection connection = CreateOpenSQLiteConnection();
            string deleteQuery = "DELETE FROM habit_logs WHERE habit_logs_entry_date = @entryDate;";
            using var command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.Add("@entryDate", System.Data.DbType.DateTime).Value = date.ToDateTime(new TimeOnly(0, 0));
            Log? searchLog = FindLogBasedOnEntryDate(date);

            if (command.ExecuteNonQuery() <= 0)
            {
                throw new InvalidDataException($"No data could be found for the entered date - {date}, unable to delete data");
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
        }
    }

    public List<Log> FindAllLogs()
    {
        List<Log> logs = new List<Log>();
        try
        {
            using SQLiteConnection connection = CreateOpenSQLiteConnection();
            string selectQuery = "SELECT * FROM habit_logs;";
            using var command = new SQLiteCommand(selectQuery, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                logs.Add(new Log(dateOfEntry: DateOnly.FromDateTime(reader.GetDateTime(1)), quantity: reader.GetInt32(2)));
            }
        }
        catch (Exception ex) when (ex is SQLiteException || ex is FormatException)
        {
            Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
        }
        return logs;
    }

    public void InsertLog(Log log)
    {
        try
        {
            using SQLiteConnection connection = CreateOpenSQLiteConnection();
            string insertQuery = "INSERT INTO habit_logs (habit_logs_entry_date, habit_logs_count) VALUES (@entryDate, @count);";
            using var command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.Add("@count", System.Data.DbType.Int32).Value = log.Quantity;
            command.Parameters.Add("@entryDate", System.Data.DbType.DateTime).Value = log.DateOfEntry.ToDateTime(new TimeOnly(0, 0));
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new InvalidDataException($"Unable to insert data");
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
        }
    }

    public void UpdateLog(Log log)
    {
        try
        {
            using SQLiteConnection connection = CreateOpenSQLiteConnection();
            string updateQuery = "UPDATE habit_logs SET habit_logs_count = @newCount WHERE habit_logs_entry_date = @entryDate;";
            using var command = new SQLiteCommand(updateQuery, connection);
            command.Parameters.Add("@newCount", System.Data.DbType.Int32).Value = log.Quantity;
            command.Parameters.Add("@entryDate", System.Data.DbType.DateTime).Value = log.DateOfEntry.ToDateTime(new TimeOnly());
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new InvalidDataException($"No data could be found for the entered date - {log.DateOfEntry}, unable to update data");
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
        }
    }

    private SQLiteConnection CreateOpenSQLiteConnection()
    {
        SQLiteConnection connection = new(_connectionString);
        connection.Open();
        return connection;
    }

    private void createTableIfDoesntExist()
    {
        try
        {
            using SQLiteConnection connection = CreateOpenSQLiteConnection();
            string createTableSQL = @"
                    CREATE TABLE IF NOT EXISTS habit_logs (
                        habit_logs_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        habit_logs_entry_date TEXT NOT NULL,
                        habit_logs_count INTEGER NOT NULL
                        );
                    ";
            using SQLiteCommand command = new SQLiteCommand(createTableSQL, connection);
            command.ExecuteNonQuery();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
        }
    }

    private Log? FindLogBasedOnEntryDate(DateOnly date)
    {
        List<Log> logs = new List<Log>();
        try
        {
            using SQLiteConnection connection = CreateOpenSQLiteConnection();
            string findLogBasedOnEntryDateSQL = @"
                                    SELECT * FROM habit_logs
                                    WHERE habit_logs_entry_date = @entryDate";
            using SQLiteCommand command = new SQLiteCommand(findLogBasedOnEntryDateSQL, connection);
            command.Parameters.Add("@entryDate", System.Data.DbType.DateTime).Value = date.ToDateTime(new TimeOnly(0, 0));
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                logs.Add(new Log(dateOfEntry: DateOnly.FromDateTime(reader.GetDateTime(1)), quantity: reader.GetInt32(2)));
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
        }
        return logs.Count == 1 ? logs[0] : null;
    }
}