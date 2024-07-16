using System.Collections.Generic;
using System.Linq;

namespace SBaier.AI
{
    public class BasicSelectAction : SelectAction
    {
        private readonly IEnumerable<Node> _children;

        public BasicSelectAction(IEnumerable<Node> children)
        {
            _children = children;
        }

        public override bool Run()
        {
            return _children.Any(child => child.Execute());
        }
    }
}