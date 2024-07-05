using System.Collections.Generic;
using System.Linq;

namespace SBaier.AI
{
    public class Sequence : NodeBase
    {
        private List<Node> _children = new List<Node>();

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
            return _children.All(child => child.Execute());
        }
    }
}