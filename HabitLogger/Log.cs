namespace HabitLogger;

internal class Log
{
    public Log(DateOnly dateOfEntry, int quantity) { DateOfEntry = dateOfEntry; Quantity = quantity; }
   
    public DateOnly DateOfEntry { get; init; }

    public int Quantity { get; init; }
}