using UnityEngine;

namespace ApplyYourself
{
    public class SandboxPhaseManager : MonoBehaviour
    {
        [SerializeField] private Timer timer = default;
        [SerializeField] private TextWriter timerText = default;
        [SerializeField] private Algorithm algorithm = default;

        private readonly FSM fsm = new FSM();

        private void Start()
        {
            timer.OnDone += algorithm.CompleteSandboxPhase;
            timer.OnNewTime += timerText.WriteTime;

            timer.Begin();

            /*DialogueSystem dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            Player player = FindAnyObjectByType<Player>();
            
            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            fsm.AddState(player);
            fsm.AddState(dialogueSystem);

            fsm.Open(player);*/
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
