namespace SBaier.AI
{
    public interface Node
    {
        int Id { get; }
        string Name { get; }
        bool Execute();
        string GetInfo();
    }
}
