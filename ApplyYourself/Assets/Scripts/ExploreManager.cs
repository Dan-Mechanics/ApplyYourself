using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class ExploreManager : MonoBehaviour
    {
        [SerializeField] private TalkQuest talkQuest = default;
        [SerializeField] private GoToQuest goToPortal = default;
        [SerializeField] private string spawnpointTag = default;
        [SerializeField] private string mainCameraTag = default;
        [SerializeField] private List<GameObject> prefabs = default;

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
        private GameObject spawnpoint;
        private GameObject cam;
        private bool isValid;

        private void Awake()
        {
            prefabs.ForEach(x => Spawn(x));

            isValid = ValidateScene();
            if (!isValid) 
            {
                enabled = false;
                Debug.LogWarning("Validation failed. Check error messages or message Tim.");
            }
        }

        private void Spawn(GameObject prefab)
        {
            Instantiate(prefab,
                prefab.transform.position,
                prefab.transform.rotation).name = prefab.name;
        }

        private bool ValidateScene()
        {
            questHandler = FindAnyObjectByType<QuestHandler>();
            dialogueSystem = FindAnyObjectByType<DialogueSystem>();
            player = FindAnyObjectByType<Player>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();
            easyText = FindAnyObjectByType<EasyText>();

            Fade[] fades = FindObjectsByType<Fade>(FindObjectsSortMode.None);
            fadeIn = fades[0];
            fadeOut = fades[1];

            portal = FindAnyObjectByType<Portal>();
            if (!portal)
            {
                Debug.LogError($"Please have a valid '{nameof(Portal)}' script in the scene.");
                return false;
            }

            spawnpoint = GameObject.FindWithTag(spawnpointTag);
            if (!spawnpoint)
            {
                Debug.LogError($"Please have a GameObject with '{spawnpointTag}' tag in the scene.");
                return false;
            }

            cam = GameObject.FindWithTag(mainCameraTag);
            if (!cam)
            {
                Debug.LogError($"Please have a GameObject with '{mainCameraTag}' tag in the scene.");
                return false;
            }

            goToPortal = new GoToQuest(player.transform, portal.transform);
            return true;
        }

        private void Start()
        {
            if (isValid)
                Setup();
        }

        private void Setup()
        {
            spawnpoint.name = spawnpointTag.ToLowerInvariant();
            player.Setup(questHandler, easyText,
                    spawnpoint.transform.position, cam.transform, sensitivityMouse, wasd);

            dialogueSystem.Setup();

            fsm.AddTransition(new StateTransition(dialogueSystem, player));
            fsm.AddState(player);
            fsm.AddState(fadeOut);
            fsm.AddState(dialogueSystem);

            dialogueSystem.OnDialogue += talkQuest.OnDialogue;
            talkQuest.OnQuestFinished += DeregisterTalkQuest;

            questHandler.AddQuest(talkQuest);
            questHandler.AddQuest(goToPortal);
            questHandler.BeginQuest();

            portal.OnInteract += BeginGoToSandboxScene;
            fadeOut.OnFadeComplete += portal.SwitchScenes;

            fsm.Open(player);
            fadeIn.BeginFade(true);
        }

        private void BeginGoToSandboxScene()
        {
            print(nameof(BeginGoToSandboxScene));
            portal.OnInteract -= BeginGoToSandboxScene;
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