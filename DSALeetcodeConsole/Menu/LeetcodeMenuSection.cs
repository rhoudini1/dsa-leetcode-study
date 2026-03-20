using System.Reflection;
using Spectre.Console;

namespace DSALeetcodeConsole.Menu;

public class LeetcodeMenuSection : IMenuSection
{
    private const string TargetNamespace = "DSALeetcodeConsole.Leetcode";

    public string Title => "Leetcode";

    private List<(string Name, MethodInfo Solver)> _problems = [];

    public bool HasItems
    {
        get
        {
            LoadProblems();
            return _problems.Count > 0;
        }
    }

    private bool _loaded = false;

    private void LoadProblems()
    {
        if (_loaded) return;
        _loaded = true;

        _problems = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == TargetNamespace && t.IsClass && t.IsAbstract && t.IsSealed) // static classes
            .Select(t =>
            {
                var nameField = t.GetField("Name", BindingFlags.Public | BindingFlags.Static);
                var solveMethod = t.GetMethod("SolveTests", BindingFlags.Public | BindingFlags.Static);
                if (nameField == null || solveMethod == null) return ((string?)null, (MethodInfo?)null);
                return ((string?)nameField.GetValue(null), solveMethod);
            })
            .Where(pair => pair.Item1 != null && pair.Item2 != null)
            .Select(pair => (pair.Item1!, pair.Item2!))
            .OrderBy(p => p.Item1)
            .ToList();
    }

    public void Run()
    {
        LoadProblems();

        var choices = _problems.Select(p => p.Name).ToList();
        choices.Add("[grey]← Back[/]");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]Select a problem:[/]")
                .PageSize(15)
                .HighlightStyle(new Style(Color.Green))
                .AddChoices(choices)
        );

        if (selection == "[grey]← Back[/]") return;

        var problem = _problems.First(p => p.Name == selection);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule($"[bold green]{problem.Name}[/]").LeftJustified());
        AnsiConsole.WriteLine();

        problem.Solver.Invoke(null, null);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[grey]Press any key to return to menu...[/]");
        Console.ReadKey(intercept: true);
    }
}
