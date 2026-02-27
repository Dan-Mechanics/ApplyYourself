using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Put the sequence here. make everyhing work with heightmap and typemap to keep seperate
    /// </summary>
    public class SandboxManager : MonoBehaviour 
    {
        [SerializeField] private Timer timer = default;
        [SerializeField] private TextWriter timerText = default;
        [SerializeField] private Algorithm algorithm = default;
        [SerializeField] private Terraformer terraformer = default;
        [SerializeField] private PivotController pivotController = default;
        private readonly FSM fsm = new FSM();

        private void Start()
        {
            timer.OnNewTime += timerText.WriteTime;
            timer.OnDone += algorithm.CompleteSandboxPhase;
            timer.Begin();

            terraformer.Setup();
            pivotController.Setup();

            fsm.AddTransition(new StateTransition(terraformer, pivotController));
            fsm.AddTransition(new StateTransition(pivotController, terraformer));
            fsm.AddState(terraformer);
            fsm.AddState(pivotController);

            fsm.Open(terraformer);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();
    }
}