using UnityEngine;

namespace ApplyYourself
{
    public class ExploreManager : MonoBehaviour
    {
        [SerializeField] private int talkQuestCount = default;
        [SerializeField] private float goToQuestDist = default;

        private readonly FSM fsm = new FSM();
        private DialogueSystem dialogueSystem;
        private QuestHandler questHandler;
        private TalkQuest talkQuest;
       // private GoToQuest goToQuest;
        private Player player;

        private void Awake()
        {
            questHandler = FindAnyObjectByType<QuestHandler>();
            dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            player = FindAnyObjectByType<Player>();
          //  goToQuest = new GoToQuest(GameObject.FindWithTag("Player").transform,
         //       GameObject.FindWithTag("Portal").transform, goToQuestDist);
            talkQuest = new TalkQuest(talkQuestCount);
        }

        private void Start()
        {
            player.Assign(questHandler.GetComponent<CanvasGroup>());

            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            fsm.AddState(player);
            fsm.AddState(dialogueSystem);

            dialogueSystem.OnDialogue += talkQuest.OnDialogue;

            questHandler.AddQuest(talkQuest);
            questHandler.Assign();

            fsm.Open(player);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
