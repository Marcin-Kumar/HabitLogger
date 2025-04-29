using System.Globalization;

namespace HabitLogger;

internal class HabitLoggerManager
{
    private static HabitLoggerManager? s_instance;
    private HabitLoggerRepository _repository;

    private HabitLoggerManager(HabitLoggerRepository repository)
    {
        _repository = repository;
    }

    internal static HabitLoggerManager GetHabitLoggerManager(HabitLoggerRepository repository)
    {
        if (s_instance == null)
        {
            s_instance = new HabitLoggerManager(repository);
        }
        return s_instance;
    }

    internal void Run()
    {
        char userEntry;
        bool exitApp = false;
        while (!exitApp)
        {
            showMenu();
            userEntry = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();
            try
            {
                switch (userEntry)
                {
                    case 'i':
                        ExecuteInsertProcess();
                        break;
                    case 'r':
                        ExecuteDeleteProcess();
                        break;
                    case 'u':
                        ExecuteUpdateProcess();
                        break;
                    case 'v':
                        ExecuteViewingProcess();
                        break;
                    case 'e':
                        exitApp = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option entered");
                        break;
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is InvalidDataException || ex is ArgumentException)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }

    private void ExecuteDeleteProcess()
    {
        DateOnly date;
        Console.WriteLine($"Please enter the date for which you would like to update the data using the format {HabitLoggerConstants.DateFormat}\n");
        string? dateEntered = Console.ReadLine();
        if (DateOnly.TryParseExact(dateEntered, HabitLoggerConstants.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            _repository.DeleteLog(date);
        }
        else
        {
            throw new ArgumentException("The date entered was invalid");
        }
    }

    private void ExecuteInsertProcess()
    {
        int quantity;
        DateOnly date;
        Console.WriteLine($"Please enter the date for which you would like to insert data using the format {HabitLoggerConstants.DateFormat}");
        string? dateEntered = Console.ReadLine();
        Console.WriteLine("Please enter the quantity for the amount of times the habit occured on the entered date");
        string? quanitityEntered = Console.ReadLine();
        if (int.TryParse(quanitityEntered, out quantity) && DateOnly.TryParseExact(dateEntered, HabitLoggerConstants.DateFormat, CultureInfo.InvariantCulture,
    DateTimeStyles.None, out date))
        {
            _repository.InsertLog(new Log(date, quantity));
        }
        else
        {
            throw new ArgumentException("Either the date or the quantity entered was in an inconsistent format");
        }
    }

    private void ExecuteUpdateProcess()
    {
        int quantity;
        DateOnly date;
        Console.WriteLine($"Please enter the date for which you would like to update the data using the format {HabitLoggerConstants.DateFormat}");
        string? dateEntered = Console.ReadLine();
        Console.WriteLine("Please enter the quantity for the amount of times the habit occured on the entered date");
        string? quanitityEntered = Console.ReadLine();
        if (int.TryParse(quanitityEntered, out quantity) && DateOnly.TryParseExact(dateEntered, HabitLoggerConstants.DateFormat, CultureInfo.InvariantCulture,
    DateTimeStyles.None, out date))
        {
            _repository.UpdateLog(new Log(date, quantity));
        }
        else
        {
            throw new ArgumentException("Either the date or the quantity entered was in an inconsistent format");
        }
    }

    private void ExecuteViewingProcess()
    {
        Console.Clear();
        List<Log> logs = _repository.FindAllLogs();
        if (logs.Count != 0)
        {
            Console.WriteLine("Log\n\nDate\t\tQuantity");
            foreach (Log log in logs)
            {
                Console.WriteLine($"{log.DateOfEntry.ToString(HabitLoggerConstants.DateFormat, CultureInfo.InvariantCulture)}\t{log.Quantity}");
            }
        }
        else
        {
            Console.WriteLine("No log records found");
        }
        Thread.Sleep(1800);
    }

    private void showMenu()
    {
        Console.WriteLine("Hello, Welcome to the Habit Logger app!");
        Console.WriteLine("Please choose an option from below\n");
        Console.WriteLine("i - to insert an entry");
        Console.WriteLine("r - to remove an entry");
        Console.WriteLine("u - to update an entry");
        Console.WriteLine("v - to view entries");
        Console.WriteLine("e - to exit\n");
        Console.WriteLine("Your option: ");
    }
}