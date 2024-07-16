namespace SBaier.AI
{
    public class WeightedNode : NodeBase
    {
        public float Weight { get; private set; } = 0;
        private readonly Node _baseNode;
        private readonly Node _condition;
        private readonly Weighter _weighter;
        
        public WeightedNode(Node baseNode, Weighter weighter)
        {
            _baseNode = baseNode;
            _weighter = weighter;
        }
        
        public WeightedNode(Node baseNode, Weighter weighter, Node condition)
            : this(baseNode, weighter)
        {
            _condition = condition;
        }

        public void UpdateWeight()
        {
            bool useWeight = _condition == null || _condition.Execute();
            Weight = useWeight ? _weighter.GetWeight() : float.MinValue;
        }
        
        public override bool Execute()
        {
            return _baseNode.Execute();
        }
                
        public override string GetInfo()
        {
            return $"{Name} (Weight: {Weight:F2})";
        }
    }
}