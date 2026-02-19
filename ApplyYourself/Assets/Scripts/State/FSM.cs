using System.Collections.Generic;

namespace ApplyYourself
{
    public class FSM 
    {
        public readonly List<StateBehaviour> states = new List<StateBehaviour>();
        public readonly List<StateTransition> transitions = new List<StateTransition>();
        private StateBehaviour current;

        public void RegiserState(StateBehaviour state)
        {
            state.OnYield += Yield;
            state.OnClaim += Open;
            state.OnDeregister += Deregister;
            states.Add(state);

            state.Exit();
        }

        private void Deregister(StateBehaviour state)
        {
            Yield(state);
            state.OnYield -= Yield;
            state.OnClaim -= Open;
            state.OnDeregister -= Deregister;
            states.Remove(state);

            for (int i = transitions.Count - 1; i >= 0; i--)
            {
                if (transitions[i].to == state || transitions[i].from == state)
                    transitions.RemoveAt(i);
            }
        }

        public void Update()
        {
            if (current != null)
                current.OnUpdate();
        }

        public void FixedUpdate()
        {
            if (current != null)
                current.OnFixedUpdate();
        }

        public void Open(StateBehaviour state)
        {
            if (current != null)
                current.Exit();

            current = state;
            current.Enter();
        }

        private void Yield(StateBehaviour from)
        {
            foreach (StateTransition transition in transitions)
            {
                if (transition.from != from)
                    continue;

                Open(transition.to);
                return;
            }
        }
    }
}
