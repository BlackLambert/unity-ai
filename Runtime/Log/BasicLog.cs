using System.Collections.Generic;
using System.Text;

namespace SBaier.AI
{
    public class BasicLog : Log, MutableLog
    {
        private const string _defaultHeader = "<b>AI actions execution log</b>";
        
        private string _header;
        private readonly List<LogEntry> _entries = new List<LogEntry>();
        
        public BasicLog(string header)
        {
            _header = header;
        }
        
        public BasicLog()
        {
            _header = _defaultHeader;
        }

        public void SetHeader(string header)
        {
            _header = header;
        }

        public void Add(LogEntry entry)
        {
            _entries.Add(entry);
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_header);
            for (int index = 0; index < _entries.Count; index++)
            {
                LogEntry entry = _entries[index];
                builder.Append($"\n{index + 1}. {entry}");
            }

            return builder.ToString();
        }
    }
}