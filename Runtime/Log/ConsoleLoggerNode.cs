using UnityEngine;

namespace SBaier.AI
{
    public class ConsoleLoggerNode : NodeBase
    {
        private readonly Node _baseNode;
        private readonly MutableLog _log;
        private bool _enabled = true;

        public ConsoleLoggerNode(Node baseNode, MutableLog log)
        {
            _baseNode = baseNode;
            _log = log;
        }

        public void EnableLogging(bool enable)
        {
            _enabled = enable;
        }
        
        public override bool Execute()
        {
            bool executed = _baseNode.Execute();

            if (_enabled)
            {
                Debug.Log(_log.ToString());
            }
            
            _log.Clear();
            return executed;
        }
    }
}