namespace SBaier.AI
{
    public class LogEntry
    {
        private string _text;

        public LogEntry()
        {
            
        }

        public LogEntry(string text)
        {
            _text = text;
        }

        public void SetText(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}