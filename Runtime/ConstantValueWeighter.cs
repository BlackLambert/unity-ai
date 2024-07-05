namespace SBaier.AI
{
    public class ConstantValueWeighter : Weighter
    {
        private float _weight;
        
        public ConstantValueWeighter(float weight)
        {
            _weight = weight;
        }
        
        public float GetWeight()
        {
            return _weight;
        }
    }
}