namespace SBaier.AI
{
    public class LoggerNode : NodeBase
    {
        private readonly Log _log;
        private readonly Node _baseNode;
        private bool _enabled = true;

        public LoggerNode(Log log, Node baseNode)
        {
            _log = log;
            _baseNode = baseNode;
        }

        public void EnableLogging(bool enable)
        {
            _enabled = enable;
        }
        
        public override bool Execute()
        {
            if (!_enabled)
            {
                return _baseNode.Execute();
            }
            
            LogEntry entry = new LogEntry();
            _log.Add(entry);
            bool executed = _baseNode.Execute();
            entry.SetText($"{_baseNode.GetInfo()} <i>{executed}</i>");
            return executed;
        }
    }
}