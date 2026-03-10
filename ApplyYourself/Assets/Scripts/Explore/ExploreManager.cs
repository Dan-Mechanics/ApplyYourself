using UnityEngine;

namespace ApplyYourself
{
    public class ExploreManager : MonoBehaviour
    {
       //[SerializeField] private int talkQuestCount = default;
       //[SerializeField] private float goToQuestDist = default;
        [SerializeField] private TalkQuest talkQuest = default;
        [SerializeField] private GoToQuest goToPortal = default;

        private readonly FSM fsm = new FSM();
        private DialogueSystem dialogueSystem;
        private QuestHandler questHandler;
        private Player player;

        private void Awake()
        {
            questHandler = FindAnyObjectByType<QuestHandler>();
            dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            player = FindAnyObjectByType<Player>();

            goToPortal = new GoToQuest(GameObject.FindWithTag(goToPortal.playerTag)?.transform,
                GameObject.FindWithTag(goToPortal.targetTag)?.transform);
        }

        private void Start()
        {
            player.Assign(questHandler);

            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            fsm.AddState(player);
            fsm.AddState(dialogueSystem);

            dialogueSystem.OnDialogue += talkQuest.OnDialogue;
            talkQuest.OnDone += DeregisterTalkQuest;

            questHandler.AddQuest(talkQuest);
            questHandler.AddQuest(goToPortal);
            questHandler.BeginQuest();

            fsm.Open(player);
        }

        private void DeregisterTalkQuest(IQuest quest)
        {
            dialogueSystem.OnDialogue -= talkQuest.OnDialogue;
            quest.OnDone -= DeregisterTalkQuest;
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
