using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTracker.mrgee1978.DataAccessLayer;

public class DeleteData
{
    /// <summary>
    /// Deletes a coding session based on the id of the session 
    /// </summary>
    /// <param name="id"></param>
    public int DeleteSession(int id)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseInitialization.DatabaseConnectionString))
            {
                connection.Open();

                string deleteStatement = "DELETE FROM sessions WHERE Id = @Id";

                // Get the number of rows affected so that you can check to make
                // sure that the row was actually deleted
                return connection.Execute(deleteStatement, new { Id = id });
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
            return -1;
        }
    }
}
