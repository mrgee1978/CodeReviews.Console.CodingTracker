using Microsoft.Data.Sqlite;
using Dapper;
using CodingTracker.mrgee1978.DomainLayer.Models;

namespace CodingTracker.mrgee1978.DataAccessLayer;

public class UpdateData
{
    /// <summary>
    /// Updates a coding session in the database using a 
    /// coding session object
    /// </summary>
    /// <param name="session"></param>
    public bool UpdateSession(CodingSession session)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseInitialization.DatabaseConnectionString))
            {
                connection.Open();

                string updateStatement = @"UPDATE sessions SET 
                    StartDate = @StartDate, EndDate = @EndDate WHERE Id = @Id";

                connection.Execute(updateStatement, new {session.Id, session.StartDate, session.EndDate});
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
