using Microsoft.Extensions.Configuration;

namespace HabitLogger;
internal class Program
{
    static void Main(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if(connectionString == null)
        {
            Console.WriteLine("Please set the database connection string, contact developer");
            return;
        }
        HabitLoggerManager.GetHabitLoggerManager(new HabitLoggerSQLiteRepository(connectionString)).Run();   
        //HabitLoggerManager.GetHabitLoggerManager(new HabitLoggerInMemoryListRepository()).Run();   
    }
}
