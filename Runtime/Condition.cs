namespace SBaier.AI
{
    public class Condition : Node
    {
        private System.Func<bool> condition;

        public Condition(System.Func<bool> condition)
        {
            this.condition = condition;
        }

        public override bool Execute()
        {
            return condition();
        }
    }
}