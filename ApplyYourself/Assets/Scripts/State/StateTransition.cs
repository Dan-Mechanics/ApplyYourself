namespace ApplyYourself
{
    public struct StateTransition
    {
        public StateBehaviour from;
        public StateBehaviour to;

        public StateTransition(StateBehaviour from, StateBehaviour to)
        {
            this.from = from;
            this.to = to;
        }
    }
}
