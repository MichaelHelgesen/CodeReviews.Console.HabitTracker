namespace habitTracker.MichaelHelgesen.Controllers;

using habitTracker.MichaelHelgesen.Models;
using habitTracker.MichaelHelgesen.Enums;
using Spectre.Console;

class MenuController
{
    internal static void RenderMainMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold blue]Welcome[/] to [green]the Habit Tracker[/]!");

            var mainMenuChoice = DisplayMainMenu();

            switch (mainMenuChoice)
            {
                case AppChoice.ViewHabits:
                    DisplayHabits("Please choose [green]a habit[/] from the list below");
                    break;
                case AppChoice.LogHabit:
                    DisplayHabitsForLog("Choose a habit to log");
                    break;
            }
        }
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

    private static void DisplayHabits(string message)
    {
        var habits = HabitRepository.GetUniqueHabits();

        var menuChoice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title(message)
            .AddChoices("Back")
            .AddChoices(habits));
        if (menuChoice == "Back")
        {
            return;
        }
        else
        {
            RenderHabitMenu(menuChoice);
        }
    }

    private static void DisplayHabitsForLog(string message)
    {
        var habits = HabitRepository.GetUniqueHabits();

        var menuChoice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title(message)
            .AddChoices("Back")
            .AddChoices(habits));
        if (menuChoice == "Back")
        {
            return;
        }
        else
        {
            RenderRegisterLogMenu(menuChoice);
        }
    }

    private static void RenderRegisterLogMenu(string habitNormalized)
    {
        var habitID = HabitRepository.GetHabitByNormalizedName(habitNormalized);
        var logs = HabitRepository.GetHabitLog(habitID);
        bool confirm = false;
        DateTime standardDato;
        int antall;
        (standardDato, antall) = SpørOmRegistrering(null, null);
        do
        {

            //(standardDato, antall) = SpørOmRegistrering(standardDato, antall);
            Console.WriteLine($"Your date: {standardDato}, your ocurrance: {antall}");
            var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
           .Title("choose")
           .AddChoices(["Confirm", "Cancel", "Edit"]));
            if (menuChoice == "Confirm")
            {
                confirm = true;
            }
            else if (menuChoice == "Cancel")
            {
                return;
            } else
            {
                (standardDato, antall) = SpørOmRegistrering(standardDato, antall);
            }
        } while (!confirm);
    }

    internal static (DateTime dato, int antall) SpørOmRegistrering(DateTime? standardDato, int? standardAntall)
    {
        var dagsDato = DateTime.Now;

        string datoPromptTekst = standardDato != null
        ? "Oppgi dato:"
        : "Oppgi dato (Enter for i dag):";

        var dato = AnsiConsole.Prompt(
        new TextPrompt<DateTime>(datoPromptTekst)
        .DefaultValue(standardDato ?? dagsDato)
        .ShowDefaultValue()
        .Validate(d => d <= dagsDato
            ? ValidationResult.Success()
            : ValidationResult.Error("Dato kan ikke være i fremtiden")));

        string antallPromptTekst = standardAntall != null
        ? "Hvor mange ganger?"
        : "Hvor mange ganger (Enter for 1):";

        var antallPrompt = new TextPrompt<int>(antallPromptTekst)
            .DefaultValue(standardAntall ?? 1)
            .Validate(n => n > 0
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]Antall må være større enn 0[/]"));

        if (standardAntall != null)
        {
            antallPrompt.DefaultValue(standardAntall.Value).ShowDefaultValue();
        }

        var antall = AnsiConsole.Prompt(antallPrompt);

        return (dato, antall);
    }


    private static void RenderHabitMenu(string habitNormalized)
    {
        var habitID = HabitRepository.GetHabitByNormalizedName(habitNormalized);
        var logs = HabitRepository.GetHabitLog(habitID);

        var menuChoice = AnsiConsole.Prompt(
        new SelectionPrompt<HabitLog>()
            .Title("Please choose [green]an option[/] from the meny below")
            .UseConverter(item => item switch
                {
                    _ => $"- {habitNormalized}: {item.Ocurrence}"
                })
            .AddChoices(logs));
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
            AppChoice.LogHabit => "✅  Log a habit",
            AppChoice.AddHabit => "▶📝  Register a new habit",
            AppChoice.ViewHabits => "📃  View habits",
            _ => item.ToString()
        };
    }
}




