using Microsoft.Data.Sqlite;
using Dapper;
using CodingTracker.mrgee1978.DomainLayer.Models;

namespace CodingTracker.mrgee1978.DataAccessLayer;

public class RetrieveData
{
    /// <summary>
    /// Retrieves all coding sessions from the database and returns them as
    /// a list of coding session objects
    /// </summary>
    /// <returns></returns>
    public List<CodingSession> RetrieveSessions()
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseInitialization.DatabaseConnectionString))
            {
                connection.Open();

                string retrievalStatement = "SELECT * FROM sessions";

                List<CodingSession> sessions = connection.Query<CodingSession>(retrievalStatement).ToList();

                // Loop through the list of coding sessions to calculate the duration of each session
                foreach (CodingSession session in sessions)
                {
                    session.Duration = session.EndDate.Subtract(session.StartDate);
                }

                return sessions;
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
            return new List<CodingSession>();
        }
    }
}
