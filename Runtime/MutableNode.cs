namespace SBaier.AI
{
    public interface MutableNode : Node
    {
        new int Id { get; set; }
        new string Name { get; set; }
    }
}