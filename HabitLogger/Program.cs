namespace HabitLogger;

internal class Program
{
    static void Main(string[] args)
    {
        showMenu();
    }

    private static void showMenu()
    {
        Console.WriteLine("Hello, Welcome to the Habit Logger app!");
        Console.WriteLine("Please choose an option from below\n");
        Console.WriteLine("I - to insert an entry");
        Console.WriteLine("R - to remove an entry");
        Console.WriteLine("U - to update an entry");
        Console.WriteLine("V - to view entries");
        Console.WriteLine("E - to exit\n");
    }

}
