using UnityEngine;

namespace ApplyYourself
{
    public class ExploreManager : MonoBehaviour
    {
        private readonly FSM fsm = new FSM();

        private void Start()
        {
            DialogueSystem dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            Player player = FindAnyObjectByType<Player>();
            
            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            fsm.AddState(player);
            fsm.AddState(dialogueSystem);

            fsm.Open(player);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
