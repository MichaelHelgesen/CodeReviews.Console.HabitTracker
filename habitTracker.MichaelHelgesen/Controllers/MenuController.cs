namespace habitTracker.MichaelHelgesen.Controllers;
using habitTracker.MichaelHelgesen.Models;
using habitTracker.MichaelHelgesen.Enums;
using Spectre.Console;

class MenuController
{
    internal static void RenderMainMenu()
    {
        Console.Clear();

        AnsiConsole.MarkupLine("[bold blue]Welcome[/] to [green]the Habit Tracker[/]!");

        var mainMenuChoice = DisplayMainMenu();
    }

    private static AppChoice DisplayMainMenu()
    {
        var menuChoices = GenerateMainMenuChoices();

        var menuChoice = AnsiConsole.Prompt(
        new SelectionPrompt<AppChoice>()
            .Title("Please choose [green]an option[/] from the meny below")
            .UseConverter(item => GenerateMainMenuItem(item))
            .AddChoices(menuChoices));
        return menuChoice;
    }
        private static List<AppChoice> GenerateMainMenuChoices()
    {
        var choices = Enum.GetValues<AppChoice>().ToList();
        return choices;
    }

    private static string GenerateMainMenuItem(AppChoice item)
    {
        return item switch
        {
            AppChoice.View => "🪜  View habits",
            AppChoice.Register => "▶️  Register a new habit",
            _ => item.ToString()
        };
    }
    
}




