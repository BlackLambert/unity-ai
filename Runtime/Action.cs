namespace SBaier.AI
{
    public class Action : Node
    {
        private System.Func<bool> action;

        public Action(System.Func<bool> action)
        {
            this.action = action;
        }

        public override bool Execute()
        {
            return action();
        }
    }
}
