using System.Collections.Generic;

namespace SBaier.AI
{
    public static class NodeExtensions
    {
        public static WeightedNode Weighted(this Node baseNode, ReadonlyObservable<Weight> weight)
        {
            return new WeightedNode(baseNode, weight);
        }

        public static WeightedSelector With(this WeightedSelector selector, WeightedNode node)
        {
            selector.AddChild(node);
            return selector;
        }

        public static WeightedSelector With(this WeightedSelector selector, IEnumerable<WeightedNode> nodes)
        {
            selector.AddChildren(nodes);
            return selector;
        }

        public static Selector With(this Selector selector, Node node)
        {
            selector.AddChild(node);
            return selector;
        }

        public static Selector With(this Selector selector, IEnumerable<Node> nodes)
        {
            selector.AddChildren(nodes);
            return selector;
        }

        public static Sequence With(this Sequence sequence, Node node)
        {
            sequence.AddChild(node);
            return sequence;
        }

        public static Sequence With(this Sequence sequence, IEnumerable<Node> nodes)
        {
            sequence.AddChildren(nodes);
            return sequence;
        }
    }
}