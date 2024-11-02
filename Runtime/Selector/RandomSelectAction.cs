using System.Collections.Generic;
using Random = System.Random;

namespace SBaier.AI
{
    public class RandomSelectAction : SelectAction
    {
        private readonly List<Node> _children;
        private readonly Random _random;

        public RandomSelectAction(List<Node> children, Random random)
        {
            _children = children;
            _random = random;
        }
        
        // Source: https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm
        public override bool Run()
        {
            Node[] elements = _children.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = _random.Next(i + 1);
                if(elements[swapIndex].Execute())
                    return true;
                elements[swapIndex] = elements[i];
            }
            return false;
        }
    }
}
