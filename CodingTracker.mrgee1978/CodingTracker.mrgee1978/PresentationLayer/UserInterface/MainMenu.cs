using Spectre.Console;
using CodingTracker.mrgee1978.PresentationLayer.Enumerations;
using CodingTracker.mrgee1978.DataAccessLayer;

namespace CodingTracker.mrgee1978.PresentationLayer.UserInterface;

public static class MainMenu
{
    public static void Run()
    {
        DatabaseInitialization.InitializeDatabase();
        CodingSessionView view = new CodingSessionView();
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("Please choose one of the following options")
                .AddChoices(
                    MenuOptions.AddSession,
                    MenuOptions.UpdateSession,
                    MenuOptions.DeleteSession,
                    MenuOptions.ViewSessions,
                    MenuOptions.Quit));

            switch (menuChoice)
            {
                case MenuOptions.AddSession:
                    view.AddCodingSession();
                    break;
                case MenuOptions.UpdateSession:
                    view.UpdateCodingSession();
                    break;
                case MenuOptions.DeleteSession:
                    view.DeleteCodingSession();
                    break;
                case MenuOptions.ViewSessions:
                    view.ViewCodingSessions();
                    AnsiConsole.MarkupLine("[blue]\n\nPress any key to continue[/]");
                    Console.ReadKey();
                    break;
                case MenuOptions.Quit:
                    isRunning = false;
                    AnsiConsole.MarkupLine("[green]\n\nThank you for using the coding tracker\nPress any key to exit[/]");
                    Console.ReadKey();
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option!\nPress any key to return to the main menu[/]");
                    Console.ReadKey();
                    break;

            }
        }
    }
}
