using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class FSM 
    {
        private readonly List<StateBehaviour> states = new List<StateBehaviour>();
        private readonly List<StateTransition> transitions = new List<StateTransition>();
        private StateBehaviour current;

        public void AddTransition(StateTransition transition) => transitions.Add(transition);

        public void AddState(StateBehaviour state)
        {
            state.OnYield += Yield;
            state.OnClaim += Open;
            state.OnDeregister += DeregisterState;

            states.Add(state);
            state.Exit();
        }

        private void DeregisterState(StateBehaviour state)
        {
            state.OnYield -= Yield;
            state.OnClaim -= Open;
            state.OnDeregister -= DeregisterState;
            states.Remove(state);

            for (int i = transitions.Count - 1; i >= 0; i--)
            {
                if (transitions[i].to == state || transitions[i].from == state)
                    transitions.RemoveAt(i);
            }

            if (current == state)
                current = null;
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
            if (current == null)
                return;

            Debug.Log($"{state.gameObject.name} --> {state.GetType().Name}");
            current.Enter();
        }

        private void Yield(StateBehaviour from)
        {
            if (from != current)
                return;

            foreach (StateTransition transition in transitions)
            {
                if (transition.from != from)
                    continue;

                Open(transition.to);
                return;
            }

            Open(null);
        }
    }
}
