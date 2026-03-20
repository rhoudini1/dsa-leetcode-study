namespace DSALeetcodeConsole.Menu;

public interface IMenuSection
{
    string Title { get; }
    bool HasItems { get; }
    void Run();
}
