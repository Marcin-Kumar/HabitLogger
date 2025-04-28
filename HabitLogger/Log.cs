namespace HabitLogger;

internal class Log
{
    public Log(DateTime dateOfEntry, int quantity) { DateOfEntry = dateOfEntry; Quantity = quantity; }
   
    public required DateTime DateOfEntry { get; init; }

    public required int Quantity { get; init; }
}