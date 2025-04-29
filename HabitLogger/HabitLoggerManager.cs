namespace HabitLogger;
internal class HabitLoggerManager
{
    private static HabitLoggerManager? s_instance;
    private HabitLoggerRepository _repository;
    private HabitLoggerManager(HabitLoggerRepository repository) {
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
        while(!exitApp)
        {
            showMenu();
            userEntry = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();
            switch (userEntry) {
                case 'i':
                    {
                        try
                        {
                            int quantity;
                            DateOnly date;
                            Console.WriteLine("Please enter the date for which you would like to insert data using the format dd-MM-yyyy");
                            string? dateEntered = Console.ReadLine();
                            Console.WriteLine("Please enter the quantity for the amount of times the habit occured on the entered date");
                            string? quanitityEntered = Console.ReadLine();
                            if (int.TryParse(quanitityEntered, out quantity) && DateOnly.TryParseExact(dateEntered, "dd-MM-yyyy", out date))
                            {
                                _repository.Insert(new Log(date, quantity));
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Data already exists for date entered\n");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Either the date or the quantity entered was in an inconsistent format\n");
                        }
                        break;
                    }
                case 'r': {
                        try
                        {
                            DateOnly date;
                            Console.WriteLine("Please enter the date for which you would like to update the data using the format dd-MM-yyyy\n");
                            string? dateEntered = Console.ReadLine();
                            if (DateOnly.TryParseExact(dateEntered, "dd-MM-yyyy", out date))
                            {
                                _repository.Delete(date);
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }
                        catch (InvalidDataException)
                        {
                            Console.WriteLine($"No data in relation to the date entered could be found, unable to delete data\n");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("The date entered was invalid\n");
                        }
                        break;
                    }
                case 'u': { 
                        try
                        {
                            int quantity;
                            DateOnly date;
                            Console.WriteLine("Please enter the date for which you would like to update the data using the format dd-MM-yyyy");
                            string? dateEntered = Console.ReadLine();
                            Console.WriteLine("Please enter the quantity for the amount of times the habit occured on the entered date");
                            string? quanitityEntered = Console.ReadLine();
                            if(int.TryParse(quanitityEntered, out quantity) && DateOnly.TryParseExact(dateEntered, "dd-MM-yyyy", out date))
                            {
                                _repository.Update(new Log(date, quantity));
                            } else
                            {
                                throw new ArgumentException();
                            }
                        } catch (InvalidDataException)
                        {
                            Console.WriteLine($"No data in relation to the date entered could be found, unable to update data");
                        } catch (ArgumentException)
                        {
                            Console.WriteLine("Either the date or the quantity entered was in an inconsistent format");
                        }
                        break; 
                    }
                case 'v': 
                    {
                        Console.Clear();
                        List<Log> logs = _repository.FindAllLogs();
                        if(logs.Count != 0)
                        {
                            Console.WriteLine("Log\n\nDate\t\tQuantity");
                            foreach (Log log in logs)
                            {
                                Console.WriteLine($"{log.DateOfEntry.ToString("dd-MM-yyyy")}\t{log.Quantity}");
                            }
                        } else
                        {
                            Console.WriteLine("No log records found");
                        }
                        Thread.Sleep(1800);
                        break; 
                    }
                case 'e': 
                    {
                        exitApp = true;    
                        break; 
                    }
                default :
                    {
                        Console.WriteLine("Invalid option entered");
                        break; 
                    }
            }
        }
    }

    private static void showMenu()
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
