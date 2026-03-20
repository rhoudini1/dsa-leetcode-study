using Spectre.Console;

namespace DSALeetcodeConsole.Menu;

public static class MenuRunner
{
    private static readonly List<IMenuSection> Sections =
    [
        new LeetcodeMenuSection(),
        // Add new sections here, e.g.: new DataStructuresMenuSection(),
    ];

    public static void Run()
    {
        while (true)
        {
            Console.Clear();
            RenderHeader();

            var availableSections = Sections.Where(s => s.HasItems).ToList();

            if (availableSections.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No problems found. Add some files to get started![/]");
                AnsiConsole.MarkupLine("[grey]Press any key to exit...[/]");
                Console.ReadKey(intercept: true);
                return;
            }

            var choices = availableSections.Select(s => s.Title).ToList();
            choices.Add("Exit");

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]What do you want to study?[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Cyan1))
                    .AddChoices(choices)
            );

            if (selection == "Exit")
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[grey]Goodbye! Keep grinding. 💪[/]");
                break;
            }

            var section = availableSections.First(s => s.Title == selection);
            Console.Clear();
            RenderHeader();
            section.Run();
        }
    }

    private static void RenderHeader()
    {
        AnsiConsole.Write(
            new FigletText("DSA Study")
                .Centered()
                .Color(Color.Cyan1)
        );

        AnsiConsole.Write(
            new Rule("[bold grey]Data Structures & Algorithms — LeetCode Practice[/]")
                .Centered()
        );

        AnsiConsole.WriteLine();
    }
}
