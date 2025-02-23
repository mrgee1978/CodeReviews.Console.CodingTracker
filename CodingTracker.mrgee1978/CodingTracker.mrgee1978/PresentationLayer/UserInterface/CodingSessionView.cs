using Spectre.Console;
using CodingTracker.mrgee1978.DomainLayer.Models; 
using CodingTracker.mrgee1978.DataAccessLayer;
using CodingTracker.mrgee1978.PresentationLayer.Validation;

namespace CodingTracker.mrgee1978.PresentationLayer.UserInterface;

public class CodingSessionView
{
    private readonly InsertData _insert;
    private readonly UpdateData _update;
    private readonly DeleteData _delete;
    private readonly RetrieveData _retrieve;

    public CodingSessionView()
    {
        _insert = new InsertData();
        _update = new UpdateData();
        _delete = new DeleteData();
        _retrieve = new RetrieveData();
    }

    /// <summary>
    /// Adds a coding session based on user input
    /// </summary>
    public void AddCodingSession()
    {
        CodingSession session = new CodingSession();

        string dateFormat = "MM-dd-yy HH:mm";

        string message = $"Please enter a valid starting date in the Format: {dateFormat} or press 0 to return to the main menu ";

        string errorMessage = $"Invalid date entered! {message}";

        string dateString = ValidateDate.GetValidDateString(message, errorMessage, dateFormat);

        if (dateString.Equals("0")) return;

        DateTime startDate = ValidateDate.GetValidStartDate(dateString);

        message = $"Please enter a valid ending date in the Format: {dateFormat} or press 0 to return to the main menu ";

        errorMessage = $"Invalid date entered! {message}";

        dateString = ValidateDate.GetValidDateString(message, errorMessage, dateFormat);

        if (dateString.Equals("0")) return;

        errorMessage = $"Invalid date entered! Ending date must come after {startDate}\nPlease try again";

        DateTime endDate = ValidateDate.GetValidEndDate(dateString, message, errorMessage, dateFormat, startDate);

        session.StartDate = startDate;
        session.EndDate = endDate;

        if (_insert.InsertSession(session))
        {
	    if (IsAnySessions(_retrieve.RetrieveSessions()))
		{
    			ViewCodingSessions();
		}
            AnsiConsole.MarkupLine("[blue]\nCoding session added successfully!\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else
        {
            AnsiConsole.MarkupLine("[red]\nError: Unable to add coding session!\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            Console.Clear();
            return;
        }
    }

    /// <summary>
    /// Updates a coding session based on user input
    /// </summary>
    public void UpdateCodingSession()
    {
        List<CodingSession> sessions = _retrieve.RetrieveSessions();

        if (!IsAnySessions(sessions))
        {
            AnsiConsole.MarkupLine("[red]\nThere are no coding sessions currently available to update\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            return;
        }
        
        ViewCodingSessions();

        CodingSession session = new CodingSession();

        string message = "\nPlease enter the id of the coding session that you wish to update or Press '0' to return to the main menu ";
        string errorMessage = $"Please enter a valid positive integer\n{message}";
        
        string dateFormat = "MM-dd-yy HH:mm";

        int id = ValidateNumber.GetValidInteger(message, errorMessage);

        if (id == 0) return;

        while (!IsValidId(sessions, id))
        {
            ViewCodingSessions();
            AnsiConsole.MarkupLine("[red]There is no coding session with that id please try again\n[/]");
            id = ValidateNumber.GetValidInteger(message, errorMessage);
            if (id == 0) return;
        }

        message = $"Please enter an updated starting date in Format: {dateFormat} or press Enter to keep the same starting date";
        errorMessage = $"Invalid updated starting date entered!\nPlease enter a valid starting date Format: {dateFormat} ";

        string dateString = ValidateDate.GetValidDateString(message, errorMessage, dateFormat, true, false);

        DateTime updatedStartDate;

        if (string.IsNullOrEmpty(dateString))
        {
            updatedStartDate = GetSameDate(sessions, id, true);
        }
        else
        {
            updatedStartDate = ValidateDate.GetValidStartDate(dateString);
        }

        message = $"Please enter an updated ending date in Format: {dateFormat} or press Enter to keep the same ending date";
        errorMessage = $"Invalid updated ending date entered!\nPlease enter a valid ending date Format: {dateFormat} ";

        dateString = ValidateDate.GetValidDateString(message, errorMessage, dateFormat, true, false);

        DateTime updatedEndDate;

        if (string.IsNullOrEmpty(dateString))
        {
            updatedEndDate = GetSameDate(sessions, id);
        }
        else
        {
            updatedEndDate = ValidateDate.GetValidEndDate(dateString, message, errorMessage, dateFormat, updatedStartDate);
        }

        session.Id = id;
        session.StartDate = updatedStartDate;
        session.EndDate = updatedEndDate;

        if (_update.UpdateSession(session))
        {
            ViewCodingSessions();
            AnsiConsole.MarkupLine("[blue]\nCoding session updated successfully!\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else
        {
            AnsiConsole.MarkupLine("[red]\nError: Unable to update coding session!\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            Console.Clear();
            return;
        }
    }

    /// <summary>
    /// Deletes a coding session based on user input
    /// </summary>
    public void DeleteCodingSession()
    {
        List<CodingSession> sessions = _retrieve.RetrieveSessions();

        if (!IsAnySessions(sessions))
        {
            AnsiConsole.MarkupLine("[red]\nThere are no coding sessions currently available to delete\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            return;
        }

        ViewCodingSessions();

        string message = "Please enter the id of the coding session you wish to delete or press '0' to return to the main menu";
        string errorMessage = "Please enter a valid positive integer\n{message}";

        int id = ValidateNumber.GetValidInteger(message, errorMessage);

        if (id == 0) return;

        while (!IsValidId(sessions, id))
        {
            ViewCodingSessions();
            AnsiConsole.MarkupLine("[red]There is no coding session with that id please try again\n[/]");
            id = ValidateNumber.GetValidInteger(message, errorMessage);
            if (id == 0) return;
        }

        if (_delete.DeleteSession(id) >= 1)
        {
            ViewCodingSessions();
            AnsiConsole.MarkupLine("[blue]\nCoding session deleted successfully!\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else
        {
            AnsiConsole.MarkupLine("[red]\nError: Unable to delete coding session!\nPress any key to return to the main menu[/]");
            Console.ReadKey();
            Console.Clear();
            return;
        }
    }

    /// <summary>
    /// Adds a coding session based on user input
    /// </summary>
    public void ViewCodingSessions()
    {
        Console.Clear();
        List<CodingSession> sessions = _retrieve.RetrieveSessions();

        if (!IsAnySessions(sessions))
        {
            AnsiConsole.MarkupLine("[red]\nNo coding sessions available\n[/]");
            return;
        }

        AnsiConsole.MarkupLine("[underline blue]Coding sessions\n[/]");

        Table table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Start Date");
        table.AddColumn("End Date");
        table.AddColumn("Session Duration");

        foreach (CodingSession session in sessions)
        {
            // Cast Duration.TotalHours to an integer for better readability
            table.AddRow(session.Id.ToString(), session.StartDate.ToString(), session.EndDate.ToString(), $"{(int)session.Duration.TotalHours} hours {session.Duration.TotalMinutes % 60} minutes");
        }

        AnsiConsole.Write(table);
    }

    
    private bool IsAnySessions(List<CodingSession> sessions)
    {
        return sessions.Count > 0;
    }

    private bool IsValidId(List<CodingSession> sessions, int sessionId)
    {
        return sessions.Any(s => s.Id == sessionId); 
    }

    private DateTime GetSameDate(List<CodingSession> sessions, int sessionId, bool isStartDate=false)
    {
        if (isStartDate)
        {
            return sessions
            .Where(s => s.Id == sessionId)
            .Select(s => s.StartDate)
            .FirstOrDefault();
        }
        else
        {
            return sessions
                .Where(s => s.Id == sessionId)
                .Select(s => s.EndDate)
                .FirstOrDefault();
        }
        
    }
}
