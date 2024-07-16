namespace SBaier.AI
{
    public interface MutableLog : Log
    {
        void SetHeader(string header);
        void Clear();
    }
}