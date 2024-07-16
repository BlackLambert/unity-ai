using System.Collections.Generic;

namespace SBaier.AI
{
    public class Selector : NodeBase
    {
        private readonly List<Node> _children;
        private SelectAction _selectAction;

        public Selector()
        {
            _children = new List<Node>();
            _selectAction = new BasicSelectAction(_children);
        }

        public void AddChild(Node child)
        {
            _children.Add(child);
        }

        public void AddChildren(IEnumerable<Node> children)
        {
            _children.AddRange(children);
        }

        public override bool Execute()
        {
            return _selectAction.Run();
        }

        public override string GetInfo()
        {
            return $"Selector '{Name}'";
        }
    }
}