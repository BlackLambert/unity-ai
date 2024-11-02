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

        public Selector(IEnumerable<Node> children) : this()
        {
            _children = new List<Node>(children);
            _selectAction = new BasicSelectAction(_children);
        }

        public Selector(SelectAction selectAction) : this()
        {
            _children = new List<Node>();
            _selectAction = selectAction;
        }

        public Selector(IEnumerable<Node> children,
            SelectAction selectAction)
        {
            _children = new List<Node>(children);
            _selectAction = selectAction;
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