using UnityEngine;

namespace ApplyYourself
{
    public class ExploreManager : MonoBehaviour
    {
        [SerializeField] private TalkQuest talkQuest = default;
        [SerializeField] private GoToQuest goToPortal = default;

        private readonly WASD wasd = new WASD();
        private readonly FSM fsm = new FSM();
        private SensitivityMouse sensitivityMouse;
        private DialogueSystem dialogueSystem;
        private QuestHandler questHandler;
        private EasyText easyText;
        private Player player;

        private void Awake()
        {
            questHandler = FindAnyObjectByType<QuestHandler>();
            dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            player = FindAnyObjectByType<Player>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            easyText = FindAnyObjectByType<EasyText>();

            goToPortal = new GoToQuest(GameObject.FindWithTag(goToPortal.playerTag)?.transform,
                GameObject.FindWithTag(goToPortal.targetTag)?.transform);
        }

        private void Start()
        {
            player.Setup(questHandler, easyText, sensitivityMouse, wasd);
            dialogueSystem.Setup();

            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            fsm.AddState(player);
            fsm.AddState(dialogueSystem);

            dialogueSystem.OnDialogue += talkQuest.OnDialogue;
            talkQuest.OnQuestFinished += DeregisterTalkQuest;

            questHandler.AddQuest(talkQuest);
            questHandler.AddQuest(goToPortal);
            questHandler.BeginQuest();

            fsm.Open(player);
        }

        private void DeregisterTalkQuest(IQuest quest)
        {
            dialogueSystem.OnDialogue -= talkQuest.OnDialogue;
            quest.OnQuestFinished -= DeregisterTalkQuest;
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}
