namespace SBaier.AI
{
    public class Condition : NodeBase
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

        public override string GetInfo()
        {
            return $"Condition '{Name}'";
        }
    }
}