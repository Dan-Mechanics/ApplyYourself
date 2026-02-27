using UnityEngine;

namespace ApplyYourself
{
    public class SandboxManager : StateBehaviour 
    {
        [SerializeField] private Timer timer = default;
        [SerializeField] private TextWriter timerText = default;
        [SerializeField] private Algorithm algorithm = default;
        [SerializeField] private Terraformer terraformer = default;
        [SerializeField] private PivotController pivotController = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private LandManager landManager = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private LerpFollow lerpFollow = default;
        [SerializeField] private Transform target = default;
        private readonly FSM fsm = new FSM();

        private void Start()
        {
            timer.OnNewTime += timerText.WriteTime;
            timer.OnDone += algorithm.CompleteSandboxPhase;

            terraformer.Setup();
            pivotController.Setup();
            waterManager.Setup();
            landManager.Setup();

            fsm.AddTransition(new StateTransition(terraformer, pivotController));
            fsm.AddTransition(new StateTransition(pivotController, terraformer));
            fsm.AddState(terraformer);
            fsm.AddState(pivotController);
        }

        public override void Enter()
        {
            base.Enter();
            timer.Begin();

            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            lerpFollow.SetTarget(target);

            waterManager.Enter();
            landManager.Enter();

            fsm.Open(terraformer);
        }

        public override void Exit()
        {
            base.Exit();
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            lerpFollow.SetTarget(null);
            fsm.Open(null);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            waterManager.OnFixedUpdate();
            landManager.OnFixedUpdate();
            fsm.FixedUpdate();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            waterManager.OnUpdate();
            landManager.OnUpdate();
            fsm.Update();
        }
    }
}