using System.Collections.Generic;

namespace SBaier.AI
{
    public class WeightedSelector : Node
    {
        private readonly List<WeightedNode> _children;
        private SelectAction _selectAction;
        
        public WeightedSelector()
        {
            _children = new List<WeightedNode>();
            _selectAction = new BasicSelectAction(_children);
        }

        public void AddChild(WeightedNode child)
        {
            _children.Add(child);
        }

        public void AddChildren(IEnumerable<WeightedNode> children)
        {
            _children.AddRange(children);
        }

        public override bool Execute()
        {
            _children.Sort(CompareWeight);
            return _selectAction.Run();
        }

        private int CompareWeight(WeightedNode x, WeightedNode y)
        {
            return y.Weight.Value.CompareTo(x.Weight.Value);
        }
    }
}
