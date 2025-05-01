# Habit Logger

**HabitLogger** is a console-based habit tracking application built with **.NET 9** and **C#**(**Whole tech stack below**). It allows users to track the frequency of a single habit by logging the number of times they perform it on a specific date. Users can add, view, update, or delete habit logs with ease.

## 📌 Features

- Log a habit with date and repetition count
- View all habit entries
- Update a specific habit entry
- Delete a habit entry

## 🛠️ Tech Stack

- [.NET 9](https://dotnet.microsoft.com/)
- C#
- SQLite (via [ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/))
- SQL
- **Repository** and **Singleton** patterns
- appsettings.json configuration

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed

### Running the App

1. Clone the repository:
   ```cmd
   git clone https://github.com/Marcin-Kumar/HabitLogger.git
   cd HabitLogger

2. Configure the SQLite connection string in appsettings.json:
   ```json
   {
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=habit_logger.db"
    }
   }

3. Run the application:
   ```cmd
   dotnet run
