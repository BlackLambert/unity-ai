namespace SBaier.AI
{
    public class WeightedNode : Node
    {
        public ReadonlyObservable<Weight> Weight { get; private set; }
        private readonly Node _baseNode;
        
        public WeightedNode(Node baseNode, ReadonlyObservable<Weight> weight)
        {
            _baseNode = baseNode;
            Weight = weight;
        }
        
        public override bool Execute()
        {
            return _baseNode.Execute();
        }
    }
}