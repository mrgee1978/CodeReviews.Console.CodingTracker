using Microsoft.Data.Sqlite;
using Dapper;
using CodingTracker.mrgee1978.DomainLayer.Models;

namespace CodingTracker.mrgee1978.DataAccessLayer;

public class InsertData
{
    /// <summary>
    /// Inserts a coding session into the database using 
    /// a coding session object
    /// </summary>
    /// <param name="session"></param>
    public bool InsertSession(CodingSession session)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseInitialization.DatabaseConnectionString))
            {
                connection.Open();

                string insertStatement = @"INSERT INTO sessions (StartDate, EndDate) 
                    VALUES (@StartDate, @EndDate);";

                connection.Execute(insertStatement, new { session.StartDate, session.EndDate });
                return true;
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
            return false;
        }
    }
}
