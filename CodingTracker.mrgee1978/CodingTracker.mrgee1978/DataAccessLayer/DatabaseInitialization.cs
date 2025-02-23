using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTracker.mrgee1978.DataAccessLayer;

public static class DatabaseInitialization
{
    private static IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    public static string? DatabaseConnectionString { get; } = config.GetConnectionString("DatabaseConnection");

   

    /// <summary>
    /// Initializes the database and creates the necessary tables if they
    /// don't already exist
    /// </summary>
    public static void InitializeDatabase()
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseConnectionString))
            {
                connection.Open();

                string tableCreationCommand = @"
                    CREATE TABLE IF NOT EXISTS sessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL);";

                connection.Execute(tableCreationCommand);
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    
}
