using System;
using System.Collections.Generic;

namespace SBaier.AI
{
    public static class NodeExtensions
    {
        public static WeightedNode Weighted(this Node baseNode, Weighter weighter)
        {
            return new WeightedNode(baseNode, weighter);
        }
        
        public static WeightedNode Weighted(this Node baseNode, Weighter weighter, Node condition)
        {
            return new WeightedNode(baseNode, weighter, condition);
        }
        
        public static WeightedNode Weighted(this Node baseNode, float weight)
        {
            return new WeightedNode(baseNode, new ConstantValueWeighter(weight));
        }
        
        public static WeightedNode Weighted(this Node baseNode, float weight, Node condition)
        {
            return new WeightedNode(baseNode, new ConstantValueWeighter(weight), condition);
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

        public static MutableNode WithId(this MutableNode node, int id)
        {
            node.Id = id;
            return node;
        }

        public static MutableNode WithId<T>(this MutableNode node, T id) where T : Enum
        {
            node.Id = Convert.ToInt32(id);
            return node;
        }

        public static MutableNode WithName(this MutableNode node, string name)
        {
            node.Name = name;
            return node;
        }

        public static Node Logged(this Node node, Log log, bool enabled = true)
        {
            LoggerNode result = new LoggerNode(log, node);
            result.EnableLogging(enabled);
            return result;
        }

        public static Node ConsoleLogged(this Node node, MutableLog log, bool enabled = true)
        {
            ConsoleLoggerNode result = new ConsoleLoggerNode(node, log);
            result.EnableLogging(enabled);
            return result;
        }
    }
}