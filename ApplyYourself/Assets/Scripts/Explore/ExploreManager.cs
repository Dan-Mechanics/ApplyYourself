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
        private Fade fadeIn;
        private Portal portal;
        private Fade fadeOut;

        private void Awake()
        {
            questHandler = FindAnyObjectByType<QuestHandler>();
            dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            player = FindAnyObjectByType<Player>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            easyText = FindAnyObjectByType<EasyText>();
            portal = FindAnyObjectByType<Portal>();

            Fade[] fades = FindObjectsByType<Fade>(FindObjectsSortMode.None);
            fadeIn = fades[0];
            fadeOut = fades[1];

            goToPortal = new GoToQuest(player.transform, portal.transform);
        }

        private void Start()
        {
            player.Setup(questHandler, easyText, sensitivityMouse, wasd);
            dialogueSystem.Setup();

            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            //fsm.AddTransition(new StateTransition(fadeIn, player));
            fsm.AddState(player);
            fsm.AddState(fadeOut);
        //    fsm.AddState(fadeIn);
            fsm.AddState(dialogueSystem);

            dialogueSystem.OnDialogue += talkQuest.OnDialogue;
            talkQuest.OnQuestFinished += DeregisterTalkQuest;

            questHandler.AddQuest(talkQuest);
            questHandler.AddQuest(goToPortal);
            questHandler.BeginQuest();

            portal.OnRequestFade += BeginGoToNextScene;
            fadeOut.OnYield += SwitchScenes;

            fsm.Open(player);
            fadeIn.BeginFade(true);
        }

        private void SwitchScenes(StateBehaviour state)
        {
            print($"{nameof(SwitchScenes)} {state.name}");
            portal.Interact();
        }

        private void BeginGoToNextScene()
        {
            print(nameof(BeginGoToNextScene));
            fadeOut.BeginFade(false);
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
