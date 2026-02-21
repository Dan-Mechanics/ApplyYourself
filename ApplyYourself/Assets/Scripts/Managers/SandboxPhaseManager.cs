using UnityEngine;

namespace ApplyYourself
{
    public class SandboxPhaseManager : MonoBehaviour
    {
        private readonly FSM fsm = new FSM();

        private void Start()
        {
            Menu menu = FindAnyObjectByType<Menu>();
            Sandbox sandbox = FindAnyObjectByType<Sandbox>();
            
            fsm.AddTransition(new StateTransition(menu, sandbox));
            fsm.AddState(menu);
            fsm.AddState(sandbox);

            fsm.Open(menu);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
